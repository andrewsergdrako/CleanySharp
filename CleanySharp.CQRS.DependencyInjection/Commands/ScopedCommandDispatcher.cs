using CleanySharp.CQRS.Commands;
using CleanySharp.CQRS.Commands.Dispatchers;
using CleanySharp.CQRS.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanySharp.CQRS.DependencyInjection.Commands;

public class ScopedCommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        
        return handler.HandleAsync(command);
    }
}