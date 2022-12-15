using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Workouts.Application.Queries;

namespace Workouts.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutQueries, WorkoutQueries>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
