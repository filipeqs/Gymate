using Microsoft.EntityFrameworkCore;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;
using Workouts.Infrastructure.EntityConfigurations;

namespace Workouts.Infrastructure.Data;

public sealed class WorkoutContext : DbContext
{
    public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
    {
    }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkoutEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
