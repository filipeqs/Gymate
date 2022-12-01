using Microsoft.Extensions.DependencyInjection;
using Workouts.Application.Queries;

namespace Workouts.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutQueries, WorkoutQueries>();

        return services;
    }
}
