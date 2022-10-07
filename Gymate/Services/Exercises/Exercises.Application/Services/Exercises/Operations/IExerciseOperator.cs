using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations
{
    public interface IExerciseOperator<in T, Z> 
        where T : ExerciseBaseModel
        where Z : BaseExerciseResponseModel
    {
        Task<Z> RunAsync(T exerciseModel);
    }
}
