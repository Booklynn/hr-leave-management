using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.ManualMappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddManualMapper(services);
        services.AddScoped<IDispatcher, Dispatcher>();
        AddRequestHandlers(services);

        return services;
    }

    private static void AddManualMapper(IServiceCollection services)
    {
        var mapperTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryMapper<,>)))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaces = mapperType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryMapper<,>));

            services.AddScoped(interfaces, mapperType);
        }
    }
    private static void AddRequestHandlers(IServiceCollection services)
    {
        var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            services.AddScoped(interfaces, handlerType);
        }
    }
}
