using MediatR;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Application.Commands;

public class CreateWorkoutCommand : IRequest<Workout>
{
    public CreateWorkoutCommand()
    {

    }

    public string Title { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<WorkoutExerciseDto> WorkoutExercises { get; set; }

    public record WorkoutExerciseDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}