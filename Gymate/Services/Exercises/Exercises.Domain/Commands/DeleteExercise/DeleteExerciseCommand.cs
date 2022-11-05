using Exercises.Domain.Models.Exercise;
using Exercises.Domain.Services.Exercises.Operations.ExerciseRemover;
using MediatR;

namespace Exercises.Domain.Commands.DeleteExercise
{
    public class DeleteExerciseCommand : IRequest<DeleteExerciseCommandResponse>
    {
        public DeleteExerciseCommand(DeleteExerciseDto exerciseRemoveDto)
        {
            ExerciseRemoveDto = exerciseRemoveDto;
        }

        public DeleteExerciseDto ExerciseRemoveDto { get; }
    }
}
