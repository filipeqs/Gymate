using MediatR;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Application.Commands;

public sealed class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, Workout>
{
    private readonly IWorkoutRepository _workoutRepository;

    public CreateWorkoutCommandHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<Workout> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = new Workout(request.Title, request.UserName, request.UserId);
        foreach (var exercise in request.WorkoutExercises)
            workout.AddWorkoutExercise(exercise.DayOfWeek, exercise.Name, exercise.Sets, exercise.Reps);

        _workoutRepository.Add(workout);
        await _workoutRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return workout;
    }
}
