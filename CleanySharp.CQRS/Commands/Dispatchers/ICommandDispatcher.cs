namespace CleanySharp.CQRS.Commands.Dispatchers;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}