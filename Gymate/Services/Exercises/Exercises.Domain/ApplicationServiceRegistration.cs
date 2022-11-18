using Exercises.Domain.Events;
using Exercises.Domain.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Exercises.Domain
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(typeof(MediatorEntryPoint).Assembly);
            services.AddScoped<IExerciseUpdateEventPublisher, ExerciseUpdateEventPublisher>();

            return services;
        }
    }
}
