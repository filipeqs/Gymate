using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Exercises.Api;
using Exercises.Domain.Events;
using Exercises.FunctionalTests.Events;
using Exercises.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Respawn;
using System.Data.Common;
using System.Reflection;

namespace Exercises.FunctionalTests
{
    public class ExerciseApiFactory : WebApplicationFactory<IApiMaker>, IAsyncLifetime
    {
        private readonly TestcontainerDatabase _dbContainer =
            new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration()
                {
                    Password = "localdevpassword#123",
                })
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithCleanUp(true)
                .Build();

        private DbConnection _dbConnection = default!;
        private Respawner _respawner = default!;

        public HttpClient HttpClient { get; private set; } 

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var path = Assembly.GetAssembly(typeof(ExerciseApiFactory)).Location;

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Tests.json")
                .Build();

            builder.UseContentRoot(Path.GetDirectoryName(path))
                .UseEnvironment("Test")
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.Tests.json", optional: false);
                })
                .ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ExerciseContext>));
                    services.AddDbContext<ExerciseContext>(options =>
                    {
                        options.UseSqlServer($"{_dbContainer.ConnectionString}TrustServerCertificate=true;");
                    });
                    services.AddScoped<IExerciseUpdateEventPublisher, FakeExerciseUpdateEventPublisher>();
                    services.Configure<RouteOptions>(configuration);
                });

            base.ConfigureWebHost(builder);
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            _dbConnection = new SqlConnection($"{_dbContainer.ConnectionString}TrustServerCertificate=true;");
            HttpClient = CreateClient();
            await InitializeRespawner();
        }

        private async Task InitializeRespawner()
        {
            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
            });
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
