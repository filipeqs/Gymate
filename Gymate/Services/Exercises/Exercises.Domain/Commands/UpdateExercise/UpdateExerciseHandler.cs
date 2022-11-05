using AutoMapper;
using Exercises.Core.Entities;
using Exercises.Domain.Models.Exercise;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Commands.UpdateExercise
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, UpdateExerciseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly UpdateExerciseCommandResponse _response = new();
        private Exercise _exercise;

        public UpdateExerciseHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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

            return _response;
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
