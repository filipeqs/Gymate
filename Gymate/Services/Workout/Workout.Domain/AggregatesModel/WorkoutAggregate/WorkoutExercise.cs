using Workout.Domain.SeedWork;

namespace Workout.Domain.AggregatesModel.WorkoutAggregate;

public class WorkoutExercise : Entity
{
    public DayOfWeek DayOfWeek { get; private set; }

    public string Name { get; private set; }
    public int Sets { get; private set; }
    public int Reps { get; private set; }

    public WorkoutExercise(DayOfWeek dayOfWeek, string name, int sets, int reps)
    {
        DayOfWeek = dayOfWeek;
        Name = name;
        Sets = sets;
        Reps = reps;
    }
}
