using Gmail.Application.Services.UserServices;
using Gmail.Domain.Data;
using Gmail.Domain.Repository.UserRepositorys;
using Gmail.Helpers.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Gmail.Application;

public static class GmailApplicationExtension
{
    public static IServiceCollection AddGmailApplicationExtension(this IServiceCollection services,
       string connectionString,
       string assemblyName)
    {
        services.CustomAddDBContext<GmailContext>(connectionString, assemblyName);

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
