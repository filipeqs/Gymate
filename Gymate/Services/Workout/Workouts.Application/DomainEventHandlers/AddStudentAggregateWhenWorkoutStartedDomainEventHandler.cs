using MediatR;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.Events;

namespace Workouts.Application.DomainEventHandlers;

public sealed class AddStudentAggregateWhenWorkoutStartedDomainEventHandler : INotificationHandler<WorkoutStartedDomainEvent>
{
    private readonly IStudentRepository _studentRepository;

    public AddStudentAggregateWhenWorkoutStartedDomainEventHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Handle(WorkoutStartedDomainEvent notification, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.FindAsync(notification.StudentIdentityId);
        if (IsNewStudent(student))
        {
            _studentRepository.Add(student);
            await _studentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        notification.Workout.SetStudentId(student.Id);
    }

    private bool IsNewStudent(Student? student)
    {
        return student == null;
    }
}
