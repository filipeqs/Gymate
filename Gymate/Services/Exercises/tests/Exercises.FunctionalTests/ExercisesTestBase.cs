using Exercises.Api.Extensions;
using Exercises.FunctionalTests.Extensions;
using Exercises.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Exercises.FunctionalTests
{
    public class ExercisesTestBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(ExercisesTestBase)).Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .UseEnvironment("Test")
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.Tests.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<ExercisesTestStartup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host.MigrateDbContext<ExerciseContext>((context, services) =>
            {
                var env = services.GetService<IHostEnvironment>();
                var logger = services.GetService<ILogger<ExerciseContextSeed>>();

                new ExerciseContextSeed()
                    .SeedAsync(context, env, logger)
                    .Wait();
            });

            return testServer;
        }
    }

    public static class Get
    {
        public static string Exercises = "api/v1/exercises";
    }
}
