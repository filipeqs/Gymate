using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Infrastructure.Data;

public sealed class WorkoutContextSeed
{
    public async Task SeedAsync(WorkoutContext context, IHostEnvironment env, ILogger<WorkoutContextSeed> logger)
    {
        using (context)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                if (!context.Workouts.Any())
                    context.Workouts.AddRange(GenerateWorkouts());

                if (!context.Students.Any())
                    context.Students.AddRange(Students);

                await context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Workout> GenerateWorkouts()
    {
        var workoutOne = new Workout("Workout One", 1);
        workoutOne.AddWorkoutExercise(DayOfWeek.Monday, "Push Up", 4, 10);
        workoutOne.AddWorkoutExercise(DayOfWeek.Wednesday, "Pull Up", 4, 10);
        workoutOne.AddWorkoutExercise(DayOfWeek.Friday, "Squats", 4, 10);

        var workoutTwo = new Workout("Workout Two", 2);
        workoutOne.AddWorkoutExercise(DayOfWeek.Monday, "Push Up", 4, 10);
        workoutOne.AddWorkoutExercise(DayOfWeek.Wednesday, "Pull Up", 4, 10);
        workoutOne.AddWorkoutExercise(DayOfWeek.Friday, "Squats", 4, 10);

        return new List<Workout> { workoutOne, workoutTwo };
    }

    private IEnumerable<Student> Students =
        new List<Student>
        {
            new Student("John", "Doe"),
            new Student("Michael", "Doe")
        };
}
