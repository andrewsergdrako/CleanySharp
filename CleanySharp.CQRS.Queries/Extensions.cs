using CleanySharp.CQRS.Queries.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanySharp.CQRS.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => 
                    c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        
        return services;
    }

    public static IServiceCollection AddSingletonQueryHandler(this IServiceCollection services)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        
        return services;
    }
}