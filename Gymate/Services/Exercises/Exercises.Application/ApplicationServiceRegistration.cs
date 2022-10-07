using Exercises.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Exercises.Application.Services.Exercises;
using Exercises.Application.Services.Exercises.Operations.ExerciseCreator;
using Exercises.Application.Services.Exercises.Operations.ExerciseRemover;
using Exercises.Application.Services.Exercises.Operations.ExerciseUpdator;
using Exercises.Application.Services.Exercises.Query.ExerciseByIdQuery;

namespace Exercises.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddScoped<IExerciseServices, ExerciseServices>();
            services.AddScoped<IExerciseCreator, ExerciseCreator>();
            services.AddScoped<IExerciseUpdator, ExerciseUpdator>();
            services.AddScoped<IExerciseRemover, ExerciseRemover>();

            services.AddScoped<IExerciseByIdQuery, ExerciseByIdQuery>();
            services.Decorate<IExerciseByIdQuery, ExerciseByIdQueryCached>();

            return services;
        }
    }
}
