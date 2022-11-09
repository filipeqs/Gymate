using Exercises.Domain.Dtos;
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
