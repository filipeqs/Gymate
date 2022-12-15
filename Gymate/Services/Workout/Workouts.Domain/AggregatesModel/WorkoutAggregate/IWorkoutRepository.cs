using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.WorkoutAggregate;

public interface IWorkoutRepository : IRepository<Workout>
{
    void Add(Workout workout);
}
