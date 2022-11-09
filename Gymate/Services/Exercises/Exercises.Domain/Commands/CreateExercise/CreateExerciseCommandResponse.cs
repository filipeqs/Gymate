using Exercises.Domain.Dtos;
using Exercises.Domain.Models;

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
