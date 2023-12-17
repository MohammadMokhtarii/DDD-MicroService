using Microsoft.Extensions.DependencyInjection;
using Services.Common;
using System.Reflection;

namespace Service.Common.Presentation;

public static class DependencyInjectionExtentions
{
    public static void RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.RegisterSingletonServices(assemblies);
        services.RegisterScopedServices(assemblies);
        services.RegisterTransientServices(assemblies);
    }
    private static void RegisterSingletonServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var applicationServices = assembly.GetTypes()
                                              .Where(x => x.IsClass
                                                          && x.IsPublic
                                                          && !x.IsAbstract
                                                          && x.GetCustomAttribute<SingletonInjectionAttribute>() is not null)
                                              .ToList();
            foreach (var applicationService in applicationServices)
            {
                var typeInterface = applicationService.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();
                if (typeInterface != null)
                {
                    if (typeInterface.GetCustomAttribute<ScopedInjectionAttribute>() is ScopedInjectionAttribute attr && !string.IsNullOrEmpty(attr.Key))
                        services.AddKeyedSingleton(typeInterface, attr.Key, applicationService);
                    else
                        services.AddSingleton(typeInterface, applicationService);

                }
                else
                    services.AddScoped(applicationService);
            }
        }
    }
    private static void RegisterScopedServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var applicationServices = assembly.GetTypes()
                                              .Where(x => x.IsClass
                                                          && x.IsPublic
                                                          && !x.IsAbstract
                                                          && x.GetCustomAttribute<ScopedInjectionAttribute>() is not null)
                                              .ToList();
            foreach (var applicationService in applicationServices)
            {
                var typeInterface = applicationService.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();
                if (typeInterface != null)
                {
                    if (typeInterface.GetCustomAttribute<ScopedInjectionAttribute>() is ScopedInjectionAttribute attr && !string.IsNullOrEmpty(attr.Key))
                        services.AddKeyedScoped(typeInterface, attr.Key, applicationService);
                    else
                        services.AddScoped(typeInterface, applicationService);

                }
                else
                    services.AddScoped(applicationService);
            }
        }
    }
    private static void RegisterTransientServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var applicationServices = assembly.GetTypes()
                                              .Where(x => x.IsClass
                                                          && x.IsPublic
                                                          && !x.IsAbstract
                                                          && x.GetCustomAttribute<TransientInjectionAttribute>() is not null)
                                              .ToList();
            foreach (var applicationService in applicationServices)
            {
                var typeInterface = applicationService.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();
                if (typeInterface != null)
                {
                    if (typeInterface.GetCustomAttribute<ScopedInjectionAttribute>() is ScopedInjectionAttribute attr && !string.IsNullOrEmpty(attr.Key))
                        services.AddKeyedTransient(typeInterface, attr.Key, applicationService);
                    else
                        services.AddTransient(typeInterface, applicationService);

                }
                else
                    services.AddTransient(applicationService);
            }
        }
    }
}
