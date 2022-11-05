using Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exercises.Infrastructure.Data
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext(DbContextOptions<ExerciseContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
    }
}
