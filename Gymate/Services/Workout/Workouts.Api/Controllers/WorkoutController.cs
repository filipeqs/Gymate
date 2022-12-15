using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Workouts.Application.Commands;
using Workouts.Application.Queries;

namespace Workouts.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class WorkoutController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWorkoutQueries _workoutQueries;

    public WorkoutController(
        IMediator mediator,
        IWorkoutQueries workoutQueries)
    {
        _mediator = mediator;
        _workoutQueries = workoutQueries;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<dynamic>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<dynamic>> GetAll()
    {
        var workouts = await _workoutQueries.GetWorkoutsAsync();
        return Ok(workouts);
    }

    [HttpGet("{studentId}")]
    [ProducesResponseType(typeof(IEnumerable<dynamic>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<dynamic>> GetWorkoutsForStudent(int studentId)
    {
        var workouts = await _workoutQueries.GetWorkoutsForStudentAsync(studentId);
        return Ok(workouts);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<dynamic>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Create([FromBody] CreateWorkoutCommand command)
    {
        var workouts = await _mediator.Send(command);
        return Ok(workouts);
    }
}
