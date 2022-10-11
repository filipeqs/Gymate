using AutoMapper;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseRemover
{
    public class ExerciseRemover : ExerciseOperatorBase, IExerciseRemover
    {
        private readonly ExerciseRemoveResponse _response = new();
        private Exercise? _exercise;

        public ExerciseRemover(IMapper mapper, IExerciseRepository repository) 
            : base(mapper, repository)
        {
        }

        public async Task<ExerciseRemoveResponse> RunAsync(ExerciseRemoveModel exerciseRemoveModel)
        {
            _exercise = await _repository.GetExerciseByIdAsync(exerciseRemoveModel.Id);
            if (ExerciseNotFound(_exercise))
                _response.BuildNotFoundErrorResponse(exerciseRemoveModel.Id);
            else
            {
                await RemoveExercise();

                if (_response.IsSuccess)
                    _response.BuildSuccessResponse();
                else
                    _response.BuildBadRequestErrorResponse("Failed to remove exercise");
            }

            return _response;
        }

        private async Task RemoveExercise()
        {
            _repository.DeleteExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }
    }
}
