namespace Workout.Api.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment environment)
        {
            return environment.EnvironmentName == "Local";
        }
    }
}
