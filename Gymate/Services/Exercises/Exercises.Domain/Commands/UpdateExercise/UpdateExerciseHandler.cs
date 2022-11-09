using AutoMapper;
using EventBus.Messages.Events;
using Exercises.Core.Entities;
using Exercises.Domain.Dtos;
using Exercises.Infrastructure.Repositories;
using MassTransit;
using MediatR;

namespace Exercises.Domain.Commands.UpdateExercise
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, UpdateExerciseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly UpdateExerciseCommandResponse _response = new();
        private Exercise _exercise;

        public UpdateExerciseHandler(IMapper mapper,
            IExerciseRepository repository,
            IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<UpdateExerciseCommandResponse> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            _exercise = await _repository.GetExerciseByIdAsync(request.ExerciseUpdateDto.Id);
            if (ExerciseNotFound())
                _response.BuildNotFoundErrorResponse(request.ExerciseUpdateDto.Id);
            else
            {
                await UpdateExercise(request.ExerciseUpdateDto);

                if (_response.IsSuccess == false)
                    _response.BuildBadRequestErrorResponse("Failed to update exercise");
            }

            await PublishExerciseUpdateEvent();

            return _response;
        }

        private async Task PublishExerciseUpdateEvent()
        {
            var eventMessage = new ExerciseUpdateEvent
            {
                ExerciseId = _exercise.Id,
                ExerciseName = _exercise.Name,
            };
            await _publishEndpoint.Publish(eventMessage);
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