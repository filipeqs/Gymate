using AutoMapper;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseUpdator
{
    public class ExerciseUpdator : ExerciseOperatorBase, IExerciseUpdator
    {

        private readonly ExerciseUpdateResponse _response = new();
        private Exercise? _exercise;

        public ExerciseUpdator(IMapper mapper, IExerciseRepository repository) 
            : base(mapper, repository)
        {
        }

        public async Task<ExerciseUpdateResponse> RunAsync(ExerciseUpdateModel exerciseUpdateModel)
        {
            _exercise = await _repository.GetExerciseByIdAsync(exerciseUpdateModel.Id);
            if (ExerciseNotFound(_exercise))
                _response.BuildNotFoundErrorResponse(exerciseUpdateModel.Id);
            else
            {
                await UpdateExercise(exerciseUpdateModel);

                if (_response.IsSuccess)
                    _response.BuildSuccessResponse();
                else
                    _response.BuildBadRequestErrorResponse("Failed to update exercise");
            }

            return _response;
        }

        private async Task UpdateExercise(ExerciseUpdateModel exerciseUpdateModel)
        {
            _mapper.Map(exerciseUpdateModel, _exercise);
            _repository.UpdateExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }
    }
}
