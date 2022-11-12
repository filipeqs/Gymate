using Exercises.Infrastructure.Data;
using Exercises.Infrastructure.Repositories;
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
            services.AddDbContext<ExerciseContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
