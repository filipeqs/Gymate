using Exercises.Core.Entities;

namespace Exercises.Infrastructure.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseByIdAsync(int id);
        Task<IEnumerable<Exercise>> GetExercisesByNameAsync(string name);
        Task CreateExerciseAsync(Exercise exercise);
        void UpdateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
        Task<bool> SaveAsync();
    }
}
