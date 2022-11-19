namespace Workout.Domain.Entities;

public class Workout : BaseEntity
{
    public string Title { get; private set; }
    private readonly List<WorkoutExercise> _workoutExercises;
    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    private DateTime _createdAt;

    public Workout(string title)
    {
        Title = title;
        _workoutExercises = new List<WorkoutExercise>();
        _createdAt = DateTime.UtcNow;
    }
}
