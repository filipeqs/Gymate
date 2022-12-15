using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Workouts.Domain.AggregatesModel.StudentAggregate;
using Workouts.Domain.AggregatesModel.WorkoutAggregate;
using Workouts.Infrastructure.Data;
using Workouts.Infrastructure.Repositories;

namespace Workouts.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkoutContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
