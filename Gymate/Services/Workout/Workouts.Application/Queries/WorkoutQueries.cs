using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace Workouts.Application.Queries;

public sealed class WorkoutQueries : IWorkoutQueries
{
    private string _connectionString;

    public WorkoutQueries(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<dynamic>> GetWorkoutsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QueryAsync<dynamic>(
            @"SELECT w.Id, Title, UserName, 
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
            $@"SELECT Title, UserName, DayOfWeek, Name, Sets, Reps
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

    private List<dynamic> MapStudentWorkoutItems(IEnumerable<dynamic> result)
    {
        var groupedResult = result.GroupBy(q => new { q.Title }).ToList();

        var workouts = new List<dynamic>();
        foreach (var workoutResult in groupedResult)
        {
            dynamic workout = new ExpandoObject();

            workout.Title = workoutResult.Key.Title;
            workout.Exercises = new List<dynamic>();

            foreach (var exerciseResult in workoutResult)
            {
                dynamic exercise = new ExpandoObject();
                exercise.Name = exerciseResult.Name;
                exercise.Sets = exerciseResult.Sets;
                exercise.Reps = exerciseResult.Reps;
                exercise.DayOfWeek = exerciseResult.DayOfWeek;

                workout.Exercises.Add(exercise);
            }

            workouts.Add(workout);
        }

        return workouts;
    }
}
