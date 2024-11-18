﻿using Gmail.Helpers.CacheSettings;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Gmail.Helpers.Extensions
{
    public static class ServicesExtensionscs
    {
        public static IServiceCollection CustomAddCors(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(
                        configuration.GetValue<string>("CorsOrigins")?.Split(";") ?? new[] { string.Empty })
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static IServiceCollection CustomAddDBContext<T>(this IServiceCollection services,
           string connectionString, string? migrationAssembly = null) where T : DbContext
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return services.AddDbContext<T>(options =>
            {
                options.UseNpgsql(connectionString, b =>
                {
                    if (!string.IsNullOrEmpty(migrationAssembly))
                        b.MigrationsAssembly(migrationAssembly);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                })
               .UseSnakeCaseNamingConvention();
            });
        }

        public static IServiceCollection CustomAddRedisCache(this IServiceCollection services,
            ConfigurationManager configuration, string instanceName)
        {
            services.AddSingleton<CacheSettings.RedisCache>();

            return services.AddDistributedMemoryCache()
                .AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("Redis");
                    options.InstanceName = instanceName;
                });
        }

        public static WebApplicationBuilder CustomAddSeriLog(this WebApplicationBuilder builder,
           IConfiguration configuration, string connectionString, string? tableName = "Logs")
        {
            var logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(configuration)
                                .WriteTo.PostgreSQL(
                                    connectionString: connectionString,
                                    tableName: tableName,
                                    needAutoCreateTable: true,
                                    batchSizeLimit: 1)
                                .Enrich.FromLogContext()
                                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            return builder;
        }

        public static IServiceCollection CustomAddRabbitMq(this IServiceCollection services,
            RabbitMqSettings rabbitMqSettings)
        {
            return services.AddMassTransit(mt => mt.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMqSettings!.Uri), "/", c =>
                    {
                        c.Username(rabbitMqSettings.UserName);
                        c.Password(rabbitMqSettings.Password);
                    });

                    ///cfg.ConfigureEndpoints(context);
                });
            }));
        }

    }
}
