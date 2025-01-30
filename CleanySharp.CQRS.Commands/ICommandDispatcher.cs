namespace CleanySharp.CQRS.Commands;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    Task DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand<TResult>;
}