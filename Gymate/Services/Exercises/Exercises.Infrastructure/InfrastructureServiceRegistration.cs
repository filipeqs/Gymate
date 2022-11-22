using Exercises.Infrastructure.Data;
using Exercises.Infrastructure.Repositories;
using Exercises.Infrastructure.Repositories.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
