using AutoMapper;
using Exercises.Infrastructure.Repositories;
using Exercises.Application.Models.Exercise;
using Exercises.Application.Services.Exercises.Operations.ExerciseCreator;
using Exercises.Application.Services.Exercises.Operations.ExerciseRemover;
using Exercises.Application.Services.Exercises.Operations.ExerciseUpdator;

namespace Exercises.Application.Services.Exercises
{
    public class ExerciseServices : IExerciseServices
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;
        private readonly IExerciseCreator _exerciseCreator;
        private readonly IExerciseUpdator _exerciseUpdator;
        private readonly IExerciseRemover _exerciseRemover;

        public ExerciseServices(IMapper mapper, IExerciseRepository repository,
            IExerciseCreator exerciseCreator, IExerciseUpdator exerciseUpdator, 
            IExerciseRemover exerciseRemover)
        {
            _mapper = mapper;
            _repository = repository;
            _exerciseCreator = exerciseCreator;
            _exerciseUpdator = exerciseUpdator;
            _exerciseRemover = exerciseRemover;
        }

        public async Task<IEnumerable<ExerciseDetailsModel>> GetAllExercisesAsync()
        {
            var exercises = await _repository.GetAllExercisesAsync();
            return _mapper.Map<IEnumerable<ExerciseDetailsModel>>(exercises);
        }

        public async Task<ExerciseDetailsModel?> GetExerciseByIdAsync(int id)
        {
            var exercise = await _repository.GetExerciseByIdAsync(id);
            return _mapper.Map<ExerciseDetailsModel>(exercise);
        }

        public async Task<IEnumerable<ExerciseDetailsModel>> GetExercisesByNameAsync(string name)
        {
            var exercises = await _repository.GetExercisesByNameAsync(name);
            return _mapper.Map<IEnumerable<ExerciseDetailsModel>>(exercises);
        }

        public async Task<ExerciseCreateResponse> CreateExerciseAsync(ExerciseCreateModel exerciseCreateModel) =>
            await _exerciseCreator.RunAsync(exerciseCreateModel);

        public async Task<ExerciseUpdateResponse> UpdateExerciseAsync(ExerciseUpdateModel exerciseUpdateModel) =>
            await _exerciseUpdator.RunAsync(exerciseUpdateModel);

        public async Task<ExerciseRemoveResponse> DeleteExerciseAsync(int exerciseId)
        {
            var model = new ExerciseRemoveModel(exerciseId);
            return await _exerciseRemover.RunAsync(model);
        }
    }
}
