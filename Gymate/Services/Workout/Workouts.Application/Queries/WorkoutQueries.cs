using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Workouts.Application.Models;

namespace Workouts.Application.Queries;

public sealed class WorkoutQueries : IWorkoutQueries
{
    private string _connectionString;

    public WorkoutQueries(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QueryAsync<WorkoutDto>(
            @"SELECT w.Id, Title, FirstName, LastName, 
                    DayOfWeek, Name, Reps, Sets
                FROM Workouts w
                LEFT JOIN Students s
                ON w.StudentId = s.Id
                LEFT JOIN WorkoutExercise we
                ON w.Id = we.WorkoutId"
            );
    }

    public async Task<dynamic> GetWorkoutsForStudentAsync(int studentId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            $@"SELECT Title, FirstName, LastName, DayOfWeek, Name, Sets, Reps
            FROM Workouts w
            LEFT JOIN Students s
            ON s.Id = w.StudentId
            LEFT JOIN WorkoutExercise we
            ON we.WorkoutId = w.Id
            WHERE w.StudentId = @studentId",
            new { studentId }
            );

        return MapStudentWorkoutItems(result);
    }

    private List<StudentWorkoutDto> MapStudentWorkoutItems(IEnumerable<dynamic> result)
    {
        var groupedResult = result.GroupBy(q => new { q.Title, q.DayOfWeek }).ToList();

        var workouts = new List<StudentWorkoutDto>();
        foreach (var workoutResult in groupedResult)
        {
            var workout = new StudentWorkoutDto
            {
                Title = workoutResult.Key.Title,
                DayOfWeek = workoutResult.Key.DayOfWeek,
            };
            workout.Exercises = new List<ExerciseDto>();

            foreach (var exerciseResult in workoutResult)
            {
                var exercise = new ExerciseDto
                {
                    Name = exerciseResult.Name,
                    Sets = exerciseResult.Sets,
                    Reps = exerciseResult.Reps
                };

                workout.Exercises.Add(exercise);
            }

            workouts.Add(workout);
        }

        return workouts;
    }
}
