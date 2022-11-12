namespace Exercises.Api.Extensions
{
    public static class IHostEnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment environment)
        {
            return environment.EnvironmentName == "Local";
        }
    }
}
