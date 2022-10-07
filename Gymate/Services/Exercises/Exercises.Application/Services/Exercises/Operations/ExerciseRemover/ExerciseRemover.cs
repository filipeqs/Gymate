using AutoMapper;
using System.Net;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;
using Exercises.Application.Extensions;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseRemover
{
    public class ExerciseRemover : IExerciseRemover
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly ExerciseRemoveResponse _response = new();
        private Exercise? _exercise;

        public ExerciseRemover(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseRemoveResponse> RunAsync(ExerciseRemoveModel exerciseRemoveModel)
        {
            _exercise = await _repository.GetExerciseByIdAsync(exerciseRemoveModel.Id);
            if (ExerciseNotFound())
                BuildNotFoundErrorResponse(exerciseRemoveModel.Id);
            else
            {
                await RemoveExercise();

                if (_response.IsSuccess)
                    BuildSuccessResponse();
                else
                    BuildBadRequestErrorResponse();
            }

            return _response;
        }

        private bool ExerciseNotFound() =>
            _exercise == null;

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();

        private async Task RemoveExercise()
        {
            _repository.DeleteExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }

        private void BuildSuccessResponse()
        {
            _response.IsSuccess = true;
        }

        private void BuildNotFoundErrorResponse(int exerciseId)
        {
            _response.ErrorMessage = $"Exercise with id {exerciseId} not found.";
            _response.ErrorStatusCode = HttpStatusCode.NotFound.ToInt();
        }

        private void BuildBadRequestErrorResponse()
        {
            _response.ErrorMessage = "Failed to update exercise";
            _response.ErrorStatusCode = HttpStatusCode.BadRequest.ToInt();
        }
    }
}
