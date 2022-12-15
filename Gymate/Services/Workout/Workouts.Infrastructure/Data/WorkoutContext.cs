using MediatR;
using Microsoft.EntityFrameworkCore;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;
using Workouts.Domain.SeedWork;
using Workouts.Infrastructure.EntityConfigurations;
using Workouts.Infrastructure.Extensions;

namespace Workouts.Infrastructure.Data;

public sealed class WorkoutContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public WorkoutContext(DbContextOptions<WorkoutContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkoutEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
