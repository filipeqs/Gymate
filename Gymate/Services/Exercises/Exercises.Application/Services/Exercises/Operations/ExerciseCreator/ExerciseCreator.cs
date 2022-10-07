using AutoMapper;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseCreator
{
    public class ExerciseCreator : IExerciseCreator
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly ExerciseCreateResponse _response = new();
        private Exercise? _exercise;

        public ExerciseCreator(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseCreateResponse> RunAsync(ExerciseCreateModel exerciseCreateModel)
        {
            _exercise = await CreateExercise(exerciseCreateModel);

            if (_response.IsSuccess)
                BuildSuccessResponse();
            else
               BuildErrorResponse();

            return _response;
        }

        private async Task<Exercise> CreateExercise(ExerciseCreateModel exerciseCreateModel)
        {
            var exercise = _mapper.Map<Exercise>(exerciseCreateModel);
            await _repository.CreateExerciseAsync(exercise);

            _response.IsSuccess = await SavedSuccessfully();

            return exercise;
        }

        private void BuildSuccessResponse()
        {
            _response.IsSuccess = true;
            _response.ExerciseDetailsModel = _mapper.Map<ExerciseDetailsModel>(_exercise);
        }

        private void BuildErrorResponse()
        {
            _response.ErrorMessage = "Failed to create exercise";
        }

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();
    }
}
