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
}
