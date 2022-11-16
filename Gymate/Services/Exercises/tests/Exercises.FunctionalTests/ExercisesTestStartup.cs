using Exercises.Api.Controllers;
using Exercises.Domain;
using Exercises.Domain.Mapping;
using Exercises.Infrastructure.Data;
using Exercises.Infrastructure.Repositories;
using FluentAssertions.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exercises.FunctionalTests
{
    public class ExercisesTestStartup
    {
        public IConfiguration _configuration { get; }

        public ExercisesTestStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(ExercisesController).Assembly);
            services.AddDbContext<ExerciseContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(typeof(MediatorEntryPoint).Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
