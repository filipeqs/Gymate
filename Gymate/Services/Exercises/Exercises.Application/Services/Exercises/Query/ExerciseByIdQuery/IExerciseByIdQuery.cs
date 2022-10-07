using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Query.ExerciseByIdQuery
{
    public interface IExerciseByIdQuery
    {
        Task<ExerciseDetailsModel?> RunAsync(int id);
    }
}