using Workouts.Domain.SeedWork;

namespace Workouts.Domain.AggregatesModel.StudentAggregate;

public interface IStudentRepository : IRepository<Student>
{
    void Add(Student student);
    Task<Student> FindAsync(int studentIdentityId);
    Task<Student> FindByIdAsync(int id);
}
