using Workouts.Domain.Events;
using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.WorkoutAggregate;

public class Workout : Entity, IAggregateRoot
{
    public string Title { get; private set; }

    public int? StudentId { get; private set; }

    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    private readonly List<WorkoutExercise> _workoutExercises;

    public Workout()
    {

    }

    public Workout(string title, string userName, int userId, int? studentId = null)
    {
        Title = title;
        StudentId = studentId;
        _workoutExercises = new List<WorkoutExercise>();
        AddDomainEvent(new WorkoutStartedDomainEvent(this, userId, userName));
    }

    public void SetStudentId(int studentId)
    {
        StudentId = studentId;
    }

    public void AddWorkoutExercise(DayOfWeek dayOfWeek, string name, int sets, int reps)
    {
        var workoutExercise = new WorkoutExercise(dayOfWeek, name, sets, reps);
        _workoutExercises.Add(workoutExercise);
    }
}
