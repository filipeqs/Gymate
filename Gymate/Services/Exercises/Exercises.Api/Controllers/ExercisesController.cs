﻿using ExceptionHandling.Models;
using Exercises.Domain.Commands.CreateExercise;
using Exercises.Domain.Commands.DeleteExercise;
using Exercises.Domain.Commands.UpdateExercise;
using Exercises.Domain.Dtos;
using Exercises.Domain.Queries.ExerciseById;
using Exercises.Domain.Queries.GetExerciseList;
using Exercises.Domain.Queries.GetExercisesByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exercises.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ExercisesController : BaseController
    {
        private readonly IMediator _mediator;
        private ILogger<ExercisesController> _logger;

        public ExercisesController(IMediator mediator, ILogger<ExercisesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExerciseDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExerciseDetailsDto>>> GetAllExercises()
        {
            _logger.LogInformation("Getting exercises");
            var exercises = await _mediator.Send(new GetExerciseListQuery());

            return Ok(exercises);
        }

        [HttpGet]
        [Route("{id}", Name = "GetExerciseById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ExerciseDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ExerciseDetailsDto>> GetExerciseById(int id)
        {
            var exercise = await _mediator.Send(new GetExerciseByIdQuery(id));
            if (ExerciseNotFound(exercise))
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, $"Exercise with id {id} not found."));

            return Ok(exercise);
        }

        [HttpGet("name/{exerciseName}")]
        [ProducesResponseType(typeof(IEnumerable<ExerciseDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExerciseDetailsDto>>> GetExercisesByName(string exerciseName)
        {
            var exercies = await _mediator.Send(new GetExercisesByNameQuery(exerciseName));
            return Ok(exercies);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ExerciseDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ExerciseDetailsDto>> CreateExercise([FromBody] CreateExerciseDto exerciseCreateModel)
        {
            var response = await _mediator.Send(new CreateExerciseCommand(exerciseCreateModel));
            if (!response.IsSuccess)
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, response.ErrorMessage));

            return CreatedAtRoute("GetExerciseById", new { id = response.ExerciseDetailsDto.Id }, response.ExerciseDetailsDto);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateExercise([FromBody] UpdateExerciseDto exerciseUpdateModel)
        {
            var response = await _mediator.Send(new UpdateExerciseCommand(exerciseUpdateModel));
            if (!response.IsSuccess)
                return StatusCode(response.ErrorStatusCode, 
                    new ApiResponse(response.ErrorStatusCode, response.ErrorMessage));

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var response = await _mediator.Send(new DeleteExerciseCommand(new DeleteExerciseDto(id)));

            if (!response.IsSuccess)
                return StatusCode(response.ErrorStatusCode, 
                    new ApiResponse(response.ErrorStatusCode, response.ErrorMessage));

            return Ok();
        }

        private bool ExerciseNotFound(ExerciseDetailsDto? exercise) =>
            exercise == null;
    }
}
