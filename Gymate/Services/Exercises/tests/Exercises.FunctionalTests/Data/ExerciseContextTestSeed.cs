using Exercises.Core.Entities;
using Exercises.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using Polly;

namespace Exercises.FunctionalTests.Data
{
    public class ExerciseContextTestSeed
    {
        public async Task SeedAsync(ExerciseContext context, IHostEnvironment env, ILogger<ExerciseContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ExerciseContextSeed));

            using (context)
            {

                if (context.Exercises.Any())
                {
                    var exervises = context.Exercises.ToList();
                    context.RemoveRange(exervises);
                }
                
                context.Exercises.AddRange(GetExercises());

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
