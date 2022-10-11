using Exercises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Exercises.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Exercises.Infrastructure.Repositories.Cache;

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

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });

            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped(typeof(ICachedRepositoryDecorator<>), typeof(CachedRepositoryDecorator<>));

            return services;
        }
    }
}
