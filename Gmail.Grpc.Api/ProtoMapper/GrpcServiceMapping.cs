using Gmail.Grpc.Api.Services;
using Gmail.Grpc.Api.Services.User;

namespace Gmail.Grpc.Api.ProtoMapper;

public static class GrpcServiceMapping
{
    public static void MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<UserService>();
    }
}
