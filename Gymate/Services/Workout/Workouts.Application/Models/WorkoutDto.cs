namespace Workouts.Application.Models;

public sealed class WorkoutDto
{
    public string Title { get; private set; }
    public List<WorkoutExerciseDto> WorkoutExercises { get; set; }
}
