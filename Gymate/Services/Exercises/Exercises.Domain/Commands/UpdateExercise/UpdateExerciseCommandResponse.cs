using Exercises.Domain.Models.Exercise;

namespace Exercises.Domain.Commands.UpdateExercise
{
    public class UpdateExerciseCommandResponse : BaseExerciseCommandResponse
    {
        public UpdateExerciseCommandResponse()
        {
            IsSuccess = false;
        }
    }
}
