namespace Exercises.FunctionalTests.Helpers
{
    public static class ExerciseRoutes
    {
        public static class Get
        {
            public static string Exercises = "api/v1/exercises";
            public static string ExerciseById = "api/v1/exercises";
            public static string ExercisesByName = "api/v1/exercises/name";
        }

        public static class Post
        {
            public static string CreateExercise = "api/v1/exercises";
        }

        public static class Put
        {
            public static string UpdateExercise = "api/v1/exercises";
        }

        public static class Delete
        {
            public static string DeleteExercise = "api/v1/exercises";
        }
    }
}
