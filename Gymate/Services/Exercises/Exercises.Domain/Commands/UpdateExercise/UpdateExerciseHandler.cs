﻿using AutoMapper;
using Exercises.Core.Entities;
using Exercises.Domain.Dtos;
using Exercises.Domain.Events;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Commands.UpdateExercise
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, UpdateExerciseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly IExerciseUpdateEventPublisher _exerciseUpdateEventPublisher;
        private readonly UpdateExerciseCommandResponse _response = new();
        private Exercise _exercise;

        public UpdateExerciseHandler(IMapper mapper,
            IExerciseRepository repository,
            IExerciseUpdateEventPublisher exerciseUpdateEventPublisher)
        {
            _mapper = mapper;
            _repository = repository;
            _exerciseUpdateEventPublisher = exerciseUpdateEventPublisher;
        }

        public async Task<UpdateExerciseCommandResponse> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            _exercise = await _repository.GetExerciseByIdAsync(request.ExerciseUpdateDto.Id);
            if (ExerciseNotFound())
            {
                _response.BuildNotFoundErrorResponse(request.ExerciseUpdateDto.Id);
                return _response;
            }

            var shouldRaiseEvent = _exercise.Name != request.ExerciseUpdateDto.Name;
            await UpdateExercise(request.ExerciseUpdateDto);

            if (_response.IsSuccess is false)
                _response.BuildBadRequestErrorResponse("Failed to update exercise");

            if (shouldRaiseEvent)
                await PublishExerciseUpdateEvent();

            return _response;
        }

        private async Task PublishExerciseUpdateEvent()
        {
            await _exerciseUpdateEventPublisher.Publish(_exercise);
        }

        private async Task UpdateExercise(UpdateExerciseDto exerciseUpdateModel)
        {
            _mapper.Map(exerciseUpdateModel, _exercise);
            _repository.UpdateExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }

        private bool ExerciseNotFound() =>
            _exercise == null;

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();
    }
}