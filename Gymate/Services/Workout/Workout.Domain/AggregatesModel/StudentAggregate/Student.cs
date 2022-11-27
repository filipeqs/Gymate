using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.StudentAggregate;

public class Student : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
