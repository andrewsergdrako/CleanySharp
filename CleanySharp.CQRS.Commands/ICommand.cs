namespace CleanySharp.CQRS.Commands;

public interface ICommand;
public interface ICommand<TResult> : ICommand;