using Workouts.Domain.AggregatesModel.WorkoutAggregate;
using Workouts.Domain.SeedWork;
using Workouts.Infrastructure.Data;

namespace Workouts.Infrastructure.Repositories
{
    public sealed class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public WorkoutRepository(WorkoutContext context)
        {
            _context = context;
        }

        public void Add(Workout workout)
        {
            _context.Workouts.Add(workout);
        }
    }
}
