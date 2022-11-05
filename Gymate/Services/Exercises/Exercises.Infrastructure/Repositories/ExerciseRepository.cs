using Exercises.Core.Entities;
using Exercises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Exercises.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ExerciseContext _context;
        public ExerciseRepository(ExerciseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync() =>
            await _context.Exercises.ToListAsync();

        public async Task<Exercise> GetExerciseByIdAsync(int id) =>
            await _context.Exercises.FirstOrDefaultAsync(q => q.Id == id);

        public async Task<IEnumerable<Exercise>> GetExercisesByNameAsync(string name) =>
            await _context.Exercises
                .Where(q => q.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

        public async Task CreateExerciseAsync(Exercise exercise) =>
            await _context.Exercises.AddAsync(exercise);

        public void UpdateExercise(Exercise exercise) =>
            _context.Entry(exercise).State = EntityState.Modified;

        public void DeleteExercise(Exercise exercise) =>
            _context.Exercises.Remove(exercise);

        public async Task<bool> SaveAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
