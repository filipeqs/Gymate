using Exercises.Domain.Models;

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
