﻿using Exercises.Domain.Dtos;
using MediatR;

namespace Exercises.Domain.Commands.CreateExercise
{
    public class CreateExerciseCommand : IRequest<CreateExerciseCommandResponse>
    {
        public CreateExerciseCommand(CreateExerciseDto exerciseCreateDto)
        {
            ExerciseCreateDto = exerciseCreateDto;
        }

        public CreateExerciseDto ExerciseCreateDto { get; }
    }
}
