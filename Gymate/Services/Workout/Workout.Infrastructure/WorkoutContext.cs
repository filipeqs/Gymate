using Microsoft.EntityFrameworkCore;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Infrastructure;

public sealed class WorkoutContext : DbContext
{
    public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
    {
    }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Student> Students { get; set; }
}
