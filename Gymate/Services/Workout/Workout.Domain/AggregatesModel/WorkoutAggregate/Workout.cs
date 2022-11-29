using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.WorkoutAggregate;

public class Workout : Entity
{
    public string Title { get; private set; }

    public int StudentId { get; private set; }

    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    private readonly List<WorkoutExercise> _workoutExercises;

    public Workout(string title, int studentId)
    {
        Title = title;
        StudentId = studentId;
        _workoutExercises = new List<WorkoutExercise>();
    }

    public void AddWorkoutExercise(DayOfWeek dayOfWeek, string name, int sets, int reps)
    {
        var workoutExercise = new WorkoutExercise(dayOfWeek, name, sets, reps);
        _workoutExercises.Add(workoutExercise);
    }
}
