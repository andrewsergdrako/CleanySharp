using CleanySharp.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanySharp.CQRS.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => 
                    c.AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        return services;
    }

    public static IServiceCollection AddSingletonCommandDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        return services;
    }

    
}