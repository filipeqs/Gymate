namespace Gymate.Aggregator.Config
{
    public class ExerciseOperations
    {
        public static string GetExercises() => "/api/v1/Exercises";
    }

    public class UrlsConfig
    {
        public string Exercise { get; set; }
        public string Workout { get; set; }
    }
}
