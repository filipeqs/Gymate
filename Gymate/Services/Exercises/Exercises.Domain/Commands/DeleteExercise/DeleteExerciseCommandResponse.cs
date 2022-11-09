using Exercises.Domain.Models;

namespace Exercises.Domain.Commands.DeleteExercise
{
    public class DeleteExerciseCommandResponse : BaseExerciseCommandResponse
    {
        public DeleteExerciseCommandResponse()
        {
            IsSuccess = false;
        }
    }
}
