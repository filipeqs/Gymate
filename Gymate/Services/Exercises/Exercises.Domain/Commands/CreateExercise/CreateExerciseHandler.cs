using AutoMapper;
using Exercises.Core.Entities;
using Exercises.Domain.Models.Exercise;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Commands.CreateExercise
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, CreateExerciseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly CreateExerciseCommandResponse _response = new();
        private Exercise _exercise;

        public CreateExerciseHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreateExerciseCommandResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            _exercise = await CreateExercise(request.ExerciseCreateDto);

            if (_response.IsSuccess)
                _response.ExerciseDetailsDto = _mapper.Map<ExerciseDetailsDto>(_exercise);
            else
                _response.BuildBadRequestErrorResponse("Failed to create exercise");

            return _response;
        }

        private async Task<Exercise> CreateExercise(CreateExerciseDto exerciseCreateModel)
        {
            var exercise = _mapper.Map<Exercise>(exerciseCreateModel);
            await _repository.CreateExerciseAsync(exercise);

            _response.IsSuccess = await SavedSuccessfully();

            return exercise;
        }

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();
    }
}