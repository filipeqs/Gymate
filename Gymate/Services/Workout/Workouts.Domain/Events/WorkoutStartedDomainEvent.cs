using MediatR;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Domain.Events;

public sealed class WorkoutStartedDomainEvent : INotification
{
    public Workout Workout { get; set; }
    public int StudentIdentityId { get; set; }
    public string UserName { get; set; }

    public WorkoutStartedDomainEvent(Workout workout, int studentIdentityId, string userName)
    {
        Workout = workout;
        StudentIdentityId = studentIdentityId;
        UserName = userName;
    }
}
