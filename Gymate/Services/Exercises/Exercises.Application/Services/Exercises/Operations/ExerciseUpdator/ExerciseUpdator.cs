﻿using AutoMapper;
using System.Net;
using Exercises.Domain.Entities;
using Exercises.Application.Extensions;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseUpdator
{
    public class ExerciseUpdator : IExerciseUpdator
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly ExerciseUpdateResponse _response = new();
        private Exercise? _exercise;

        public ExerciseUpdator(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseUpdateResponse> RunAsync(ExerciseUpdateModel exerciseUpdateModel)
        {
            _exercise = await _repository.GetExerciseByIdAsync(exerciseUpdateModel.Id);
            if (ExerciseNotFound())
                BuildNotFoundErrorResponse(exerciseUpdateModel.Id);
            else
            {
                await UpdateExercise(exerciseUpdateModel);

                if (_response.IsSuccess)
                    BuildSuccessResponse();
                else
                    BuildBadRequestErrorResponse();
            }

            return _response;
        }

        private async Task UpdateExercise(ExerciseUpdateModel exerciseUpdateModel)
        {
            _mapper.Map(exerciseUpdateModel, _exercise);
            _repository.UpdateExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }

        private bool ExerciseNotFound() =>
            _exercise == null;

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();

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