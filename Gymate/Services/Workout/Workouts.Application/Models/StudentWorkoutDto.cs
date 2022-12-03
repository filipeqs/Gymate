namespace Workouts.Application.Models;

public sealed class StudentWorkoutDto
{
    public int DayOfWeek { get; set; }
    public string Title { get; set; }
    public List<ExerciseDto> Exercises { get; set; }
}
