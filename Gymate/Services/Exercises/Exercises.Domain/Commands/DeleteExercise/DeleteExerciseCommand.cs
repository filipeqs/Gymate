using Exercises.Domain.Models.Exercise;
using Exercises.Domain.Services.Exercises.Operations.ExerciseRemover;
using MediatR;

namespace Exercises.Domain.Commands.DeleteExercise
{
    public class DeleteExerciseCommand : IRequest<DeleteExerciseCommandResponse>
    {
        public DeleteExerciseCommand(DeleteExerciseDto deleteExerciseDto)
        {
            DeleteExerciseDto = deleteExerciseDto;
        }

        public DeleteExerciseDto DeleteExerciseDto { get; }
    }
}
