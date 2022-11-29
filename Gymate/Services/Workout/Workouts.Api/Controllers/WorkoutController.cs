using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Workouts.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class WorkoutController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            return Ok("Works!");
        }
    }
}
