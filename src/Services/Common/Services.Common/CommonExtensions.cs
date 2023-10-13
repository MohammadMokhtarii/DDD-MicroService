using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Services.Common;
public static class CommonExtensions
{

    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultHealthChecks(builder.Configuration);
        builder.Services.AddHttpContextAccessor();

        return builder;
    }
    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        var pathBase = app.Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
            app.UseRouting();
        }

        app.MapDefaultHealthChecks();
        return app;
    }


    public static IHealthChecksBuilder AddDefaultHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        var eventBusSection = configuration.GetSection("EventBus");

        if (!eventBusSection.Exists())
        {
            return hcBuilder;
        }

        hcBuilder.AddRabbitMQ(_ => $"amqp://{configuration.GetRequiredConnectionString("EventBus")}", name: "rabbitmq", tags: new string[] { "ready" });

        return hcBuilder;
    }
    public static void MapDefaultHealthChecks(this IEndpointRouteBuilder routes)
    {
        routes.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        routes.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = r => r.Name.Contains("self")
        });
    }
}
