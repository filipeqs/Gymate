namespace Workouts.Application.Models;

public class WorkoutExerciseDto
{
    public string DayOfWeek { get; private set; }
    public string Name { get; private set; }
    public int Sets { get; private set; }
    public int Reps { get; private set; }
}
