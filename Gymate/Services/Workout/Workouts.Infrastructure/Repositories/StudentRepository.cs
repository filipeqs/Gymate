using Microsoft.EntityFrameworkCore;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.SeedWork;
using Workouts.Infrastructure.Data;

namespace Workouts.Infrastructure.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly WorkoutContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public StudentRepository(WorkoutContext context)
    {
        _context = context;
    }

    public void Add(Student student)
    {
        _context.Students.Add(student);
    }

    public async Task<Student> FindAsync(int studentIdentityId) => 
        await _context.Students.SingleOrDefaultAsync(q => q.IdentityId == studentIdentityId);

    public async Task<Student> FindByIdAsync(int id) =>
        await _context.Students.FindAsync(id);
}
