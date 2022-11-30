namespace Workouts.Api.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment environment)
        {
            return environment.EnvironmentName == "Local";
        }
    }
}
