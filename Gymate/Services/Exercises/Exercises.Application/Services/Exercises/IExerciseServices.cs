using Exercises.Application.Models.Exercise;
using Exercises.Application.Services.Exercises.Operations.ExerciseCreator;
using Exercises.Application.Services.Exercises.Operations.ExerciseRemover;
using Exercises.Application.Services.Exercises.Operations.ExerciseUpdator;

namespace Exercises.Application.Services.Exercises
{
    public interface IExerciseServices
    {
        Task<IEnumerable<ExerciseDetailsModel>> GetAllExercisesAsync();
        Task<ExerciseDetailsModel?> GetExerciseByIdAsync(int id);
        Task<IEnumerable<ExerciseDetailsModel>> GetExercisesByNameAsync(string name);
        Task<ExerciseCreateResponse> CreateExerciseAsync(ExerciseCreateModel exercise);
        Task<ExerciseUpdateResponse> UpdateExerciseAsync(ExerciseUpdateModel exercise);
        Task<ExerciseRemoveResponse> DeleteExerciseAsync(int exerciseId);

    }
}
