using Gymate.Aggregator.Models;

namespace Gymate.Aggregator.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseModel>> GetExercises();
    }
}
