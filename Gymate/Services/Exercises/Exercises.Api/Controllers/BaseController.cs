using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Exercises.Api.Controllers
{
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class BaseController : ControllerBase
    {
    }
}
