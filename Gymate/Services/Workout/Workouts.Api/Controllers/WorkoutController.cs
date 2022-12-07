using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Workouts.Application.Models;
using Workouts.Application.Queries;

namespace Workouts.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutQueries _workoutQueries;

    public WorkoutController(IWorkoutQueries workoutQueries)
    {
        _workoutQueries = workoutQueries;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkoutDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<WorkoutDto>> GetAll()
    {
        var workouts = await _workoutQueries.GetWorkoutsAsync();
        return Ok(workouts);
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult> GetWorkoutsForStudent(int studentId)
    {
        var workouts = await _workoutQueries.GetWorkoutsForStudentAsync(studentId);

        return Ok(workouts);
    }
}
