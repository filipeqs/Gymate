using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.StudentAggregate;

public class Student : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Student(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
