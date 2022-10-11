using AutoMapper;
using Exercises.Domain.Entities;
using Exercises.Infrastructure.Repositories;

namespace Exercises.Application.Services.Exercises.Operations
{
    public abstract class ExerciseOperatorBase
    {
        protected readonly IMapper _mapper;
        protected readonly IExerciseRepository _repository;

        protected ExerciseOperatorBase(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        protected async Task<bool> SavedSuccessfully() =>
            await _repository.SaveAsync();

        protected bool ExerciseNotFound(Exercise? exercise) =>
            exercise == null;
    }
}
