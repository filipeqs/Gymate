namespace Workouts.Application.Queries;

public interface IWorkoutQueries
{
    Task<IEnumerable<dynamic>> GetWorkoutsAsync();
    Task<dynamic> GetWorkoutsForStudentAsync(int studentId);
}
