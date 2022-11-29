using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;

namespace Workouts.Infrastructure.EntityConfigurations;

public sealed class WorkoutEntityTypeConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasOne<Student>()
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(w => w.StudentId);
    }
}
