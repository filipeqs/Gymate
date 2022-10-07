using Exercises.Application.Models.Exercise;
using Exercises.Infrastructure.Repositories.Cache;

namespace Exercises.Application.Services.Exercises.Query.ExerciseByIdQuery
{
    public class ExerciseByIdQueryCached : IExerciseByIdQuery
    {
        private readonly IExerciseByIdQuery _exerciseByIdQuery;
        private readonly ICachedRepository<ExerciseDetailsModel> _cachedRepository;

        public ExerciseByIdQueryCached(IExerciseByIdQuery exerciseByIdQuery, 
            ICachedRepository<ExerciseDetailsModel> cachedRepository)
        {
            _exerciseByIdQuery = exerciseByIdQuery;
            _cachedRepository = cachedRepository;
        }

        public async Task<ExerciseDetailsModel?> RunAsync(int id)
        {
            var exercise = await _cachedRepository.GetCachedValueAsync(id.ToString());
            if (exercise == null)
            {
                exercise = await _exerciseByIdQuery.RunAsync(id);
                if (exercise != null)
                    await _cachedRepository.UpdateCachedValueAsync(id.ToString(), exercise);
            }

            return exercise;
        }
    }
}
