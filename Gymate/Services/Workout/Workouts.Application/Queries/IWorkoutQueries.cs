using Workouts.Application.Models;

namespace Workouts.Application.Queries;

public interface IWorkoutQueries
{
    Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync();
}
