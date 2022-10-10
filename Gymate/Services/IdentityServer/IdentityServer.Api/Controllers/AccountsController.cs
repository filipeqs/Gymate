using IdentityModel;
using IdentityServer.Api.Entities;
using IdentityServer.Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityServer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
                return BadRequest("Email address already exists.");

            var user = new ApplicationUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            result = await _userManager.AddClaimsAsync(user, new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, $"{registerModel.FirstName} {registerModel.LastName}"),
                new Claim(JwtClaimTypes.GivenName, registerModel.FirstName),
                new Claim(JwtClaimTypes.FamilyName, registerModel.LastName),
                new Claim(JwtClaimTypes.Email, registerModel.Email),
                new Claim(JwtClaimTypes.Role, "User"),
                new Claim(JwtClaimTypes.Role, "Admin")
            });
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserModel() { Email = user.Email };
        }
    }
}
