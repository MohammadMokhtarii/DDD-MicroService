using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Services.Common;
public static class CommonExtensions
{

    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        builder.AddSerilog();

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


    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var serilog = builder.Configuration.GetSection("Serilog");
        if (serilog.Exists())
            builder.Host.UseSerilog((context, logger) => logger.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
    public static IHealthChecksBuilder AddDefaultHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        var eventBus = configuration.GetRequiredConnectionString("EventBus");

        if (string.IsNullOrEmpty(eventBus))
            return hcBuilder;


        hcBuilder.AddRabbitMQ(eventBus, name: "eventbus", tags: new string[] { "ready" });

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
