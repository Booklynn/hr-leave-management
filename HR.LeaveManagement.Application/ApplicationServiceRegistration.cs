using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Reflection;

namespace HR.LeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddQueryMapper(services);
        AddCreateMapper(services);
        AddUpdateMapper(services);
        services.AddScoped<IDispatcher, Dispatcher>();
        AddRequestHandlers(services);

        return services;
    }

    private static void AddQueryMapper(IServiceCollection services)
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

    private static void AddCreateMapper(IServiceCollection services)
    {
        var mapperTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreateMapper<,>)))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaces = mapperType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreateMapper<,>));

            services.AddScoped(interfaces, mapperType);
        }
    }

    private static void AddUpdateMapper(IServiceCollection services)
    {
        var mapperTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IUpdateMapper<,>)))
            .ToImmutableArray();

        foreach (var mapperType in mapperTypes)
        {
            var interfaces = mapperType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IUpdateMapper<,>));

            services.AddScoped(interfaces, mapperType);
        }
    }

    private static void AddRequestHandlers(IServiceCollection services)
    {
        var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToImmutableArray();

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            services.AddScoped(interfaces, handlerType);
        }
    }
}
