using Exercises.Domain.Models.Exercise;

namespace Exercises.Domain.Commands.CreateExercise
{
    public class CreateExerciseCommandResponse : BaseExerciseCommandResponse
    {
        public CreateExerciseCommandResponse()
        {
            IsSuccess = false;
        }

        public ExerciseDetailsDto ExerciseDetailsDto { get; set; }
    }
}
