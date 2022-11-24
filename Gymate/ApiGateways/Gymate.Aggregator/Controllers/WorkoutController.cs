using Gymate.Aggregator.Interfaces;
using Gymate.Aggregator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymate.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public WorkoutController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ExerciseModel>> GetExercises()
        {
            var exercises = await _exerciseService.GetExercises();

            return Ok(exercises);
        }
    }
}
