using System.Net;
using Microsoft.AspNetCore.Mvc;
using Exercises.Application.Models.Exercise;
using Exercises.Application.Services.Exercises;
using Microsoft.AspNetCore.Authorization;

namespace Exercises.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ExercisesController : BaseController
    {
        private readonly IExerciseServices _exerciseServices;

        public ExercisesController(IExerciseServices exerciseServices)
        {
            _exerciseServices = exerciseServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExerciseDetailsModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExerciseDetailsModel>>> GetAllExercises()
        {
            var exercises = await _exerciseServices.GetAllExercisesAsync();
            return Ok(exercises);
        }

        [HttpGet]
        [Route("{id}", Name = "GetExerciseById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ExerciseDetailsModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ExerciseDetailsModel>> GetExerciseById(int id)
        {
            var exercise = await _exerciseServices.GetExerciseByIdAsync(id);
            if (ExerciseNotFound(exercise))
                return NotFound($"Exercise with id {id} not found.");

            return Ok(exercise);
        }

        [HttpGet("name/{exerciseName}")]
        [ProducesResponseType(typeof(IEnumerable<ExerciseDetailsModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExerciseDetailsModel>>> GetExercisesByName(string exerciseName)
        {
            var exercies = await _exerciseServices.GetExercisesByNameAsync(exerciseName);
            return Ok(exercies);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExerciseDetailsModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ExerciseDetailsModel>> CreateExercise([FromBody] ExerciseCreateModel exerciseCreateModel)
        {
            var response = await _exerciseServices.CreateExerciseAsync(exerciseCreateModel);
            if (!response.IsSuccess)
                return BadRequest(response.ErrorMessage);

            return CreatedAtRoute("GetExerciseById", new { id = response.ExerciseDetailsModel.Id }, response.ExerciseDetailsModel);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateExercise([FromBody] ExerciseUpdateModel exerciseUpdateModel)
        {
            var response = await _exerciseServices.UpdateExerciseAsync(exerciseUpdateModel);
            if (!response.IsSuccess)
                return StatusCode(response.ErrorStatusCode, response.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var response = await _exerciseServices.DeleteExerciseAsync(id);

            if (!response.IsSuccess)
                return StatusCode(response.ErrorStatusCode, response.ErrorMessage);

            return Ok();
        }

        private bool ExerciseNotFound(ExerciseDetailsModel? exercise) =>
            exercise == null;
    }
}
