using Services.Common;

namespace NotificationManagement.Api.Core;

public static class ConfigureService
{
    public static IServiceCollection AddPresentaionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(Lazy<>), typeof(Lazier<>));

        return services;
    }
}
