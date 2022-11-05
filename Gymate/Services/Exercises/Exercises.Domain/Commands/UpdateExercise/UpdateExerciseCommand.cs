﻿using Exercises.Domain.Models.Exercise;
using MediatR;

namespace Exercises.Domain.Commands.UpdateExercise
{
    public class UpdateExerciseCommand : IRequest<UpdateExerciseCommandResponse>
    {
        public UpdateExerciseCommand(UpdateExerciseDto exerciseUpdateDto)
        {
            ExerciseUpdateDto = exerciseUpdateDto;
        }

        public UpdateExerciseDto ExerciseUpdateDto { get; }
    }
}
