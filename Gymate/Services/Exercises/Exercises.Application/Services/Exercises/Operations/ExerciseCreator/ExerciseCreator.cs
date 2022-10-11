using AutoMapper;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseCreator
{
    public class ExerciseCreator : ExerciseOperatorBase, IExerciseCreator
    {
        private readonly ExerciseCreateResponse _response = new();
        private Exercise? _exercise;

        public ExerciseCreator(IMapper mapper, IExerciseRepository repository) 
            : base(mapper, repository)
        {
        }

        public async Task<ExerciseCreateResponse> RunAsync(ExerciseCreateModel exerciseCreateModel)
        {
            _exercise = await CreateExercise(exerciseCreateModel);

            if (_response.IsSuccess)
            {
                _response.BuildSuccessResponse();
                _response.ExerciseDetailsModel = _mapper.Map<ExerciseDetailsModel>(_exercise);
            }
            else
                _response.BuildBadRequestErrorResponse("Failed to create exercise");

            return _response;
        }

        private async Task<Exercise> CreateExercise(ExerciseCreateModel exerciseCreateModel)
        {
            var exercise = _mapper.Map<Exercise>(exerciseCreateModel);
            await _repository.CreateExerciseAsync(exercise);

            _response.IsSuccess = await SavedSuccessfully();

            return exercise;
        }
    }
}
