using Exercises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Exercises.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exercises.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExerciseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            return services;
        }
    }
}
