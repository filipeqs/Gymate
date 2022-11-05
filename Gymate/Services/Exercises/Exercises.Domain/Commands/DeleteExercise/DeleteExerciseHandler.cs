using AutoMapper;
using Exercises.Core.Entities;
using Exercises.Domain.Services.Exercises.Operations.ExerciseRemover;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Commands.DeleteExercise
{
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, DeleteExerciseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly DeleteExerciseCommandResponse _response = new();
        private Exercise? _exercise;

        public DeleteExerciseHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DeleteExerciseCommandResponse> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            _exercise = await _repository.GetExerciseByIdAsync(request.ExerciseRemoveDto.Id);
            if (ExerciseNotFound())
                _response.BuildNotFoundErrorResponse(request.ExerciseRemoveDto.Id);
            else
            {
                await RemoveExercise();

                if (_response.IsSuccess == false)
                    _response.BuildBadRequestErrorResponse("Failed to remove exercise");
            }

            return _response;
        }

        private bool ExerciseNotFound() =>
            _exercise == null;

        private async Task RemoveExercise()
        {
            _repository.DeleteExercise(_exercise);
            _response.IsSuccess = await SavedSuccessfully();
        }

        private async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();
    }
}
