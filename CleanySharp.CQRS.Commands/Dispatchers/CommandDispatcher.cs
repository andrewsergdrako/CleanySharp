using Microsoft.Extensions.DependencyInjection;

namespace CleanySharp.CQRS.Commands.Dispatchers;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        return handler.HandleAsync(command, cancellationToken);
    }

    public Task DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand<TResult>
    {
        var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync(command, cancellationToken);
    }
}