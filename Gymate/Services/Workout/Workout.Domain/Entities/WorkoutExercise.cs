namespace Workout.Domain.Entities;

public class WorkoutExercise : BaseEntity
{
    private DayOfWeek _day;
    private List<Exercise> _exercises;

    public WorkoutExercise(DayOfWeek day)
    {
        _day = day;
        _exercises = new List<Exercise>();
    }
}
