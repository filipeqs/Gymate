namespace Exercises.Api.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal(this IWebHostEnvironment environment)
        {
            return environment.EnvironmentName == "Local";
        }
    }
}
