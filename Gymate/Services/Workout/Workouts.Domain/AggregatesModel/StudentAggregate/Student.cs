using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.StudentAggregate;

public class Student : Entity, IAggregateRoot
{
    public int IdentityId { get; private set; }
    public string UserName { get; private set; }

    public Student()
    {

    }

    public Student(int identity, string userName)
    {
        IdentityId = identity;
        UserName = userName;
    }
}
