﻿using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Common.Logging;

public static class SeriLogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
          (context, configuration) =>
          {
              var elasticUri = context.Configuration.GetConnectionString("ElasticSearchConnection");

              configuration
                   .Enrich.FromLogContext()
                   .Enrich.WithMachineName()
                   .WriteTo.Debug()
                   .WriteTo.Console()
                   .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUri))
                        {
                            IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        })
                   .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                   .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                   .ReadFrom.Configuration(context.Configuration);
          };
}
