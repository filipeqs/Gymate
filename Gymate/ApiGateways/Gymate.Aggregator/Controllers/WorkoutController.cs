using Gymate.Aggregator.Interfaces;
using Gymate.Aggregator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gymate.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public WorkoutController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<ExerciseModel>> GetExercises()
        {
            var exercises = await _exerciseService.GetExercises();

            return Ok(exercises);
        }
    }
}
