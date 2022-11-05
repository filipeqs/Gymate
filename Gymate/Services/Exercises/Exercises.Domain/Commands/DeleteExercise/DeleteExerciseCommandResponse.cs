using Exercises.Domain.Models.Exercise;

namespace Exercises.Domain.Services.Exercises.Operations.ExerciseRemover
{
    public class DeleteExerciseCommandResponse : BaseExerciseCommandResponse
    {
        public DeleteExerciseCommandResponse()
        {
            IsSuccess = false;
        }
    }
}
