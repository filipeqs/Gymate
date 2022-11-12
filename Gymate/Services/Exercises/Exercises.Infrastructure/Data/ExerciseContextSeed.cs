using Exercises.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Exercises.Infrastructure.Data
{
    public class ExerciseContextSeed
    {
        public async Task SeedAsync(ExerciseContext context, IHostEnvironment env, ILogger<ExerciseContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ExerciseContextSeed));

            using (context)
            {
                if (env.IsDevelopment() || env.EnvironmentName == "Test")
                {
                    if (!context.Exercises.Any())
                        context.Exercises.AddRange(GetExercises());
                }

                await context.SaveChangesAsync();
            };
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ExerciseContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }

        private IEnumerable<Exercise> GetExercises()
        {
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Squat"
                },
                new Exercise
                {
                    Name = "Leg Press"
                },
                new Exercise
                {
                    Name = "Bench Press"
                },
                new Exercise
                {
                    Name = "Deadlift"
                },
                new Exercise
                {
                    Name = "Calf Raise"
                },
            };

            return exercises;
        }
    }
}
