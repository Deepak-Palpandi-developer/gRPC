using Gmail.Application;
using Gmail.Domain.Data;
using Gmail.Grpc.Api.ProtoMapper;
using Gmail.Helpers.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

configuration.AddEnvironmentVariables("GMAILCLONE_");

string connectionString = configuration.GetValue<string>("GmailConnectionString") ?? string.Empty;

string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty;

builder.Services.CustomAddCors(configuration);

builder.Services.AddGmailApplicationExtension(connectionString, assemblyName);

builder.CustomAddSeriLog(configuration, connectionString);

builder.Services.CustomAddRedisCache(configuration, "Gmail:");

builder.Services.AddGrpc();

builder.Services.AddGrpcReflection();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MigrateDatabase<GmailContext>();

app.MapGrpcReflectionService();

app.UseCors();

app.UseRouting();

app.MapGrpcServices();

app.MapGet("/", () => "Welcome to the gRPC Service! To communicate with this service, please use a gRPC client.");

app.Run();
