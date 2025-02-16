using CleanySharp.Abstractions;

namespace CleanySharp.CQRS.Commands.Decorators;

public class UowCommandHandler<TCommand>(ICommandHandler<TCommand> handler, IUnitOfWork unitOfWork) 
    : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await handler.HandleAsync(command, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}

public class UowCommandHandler<TCommand, TResult>(ICommandHandler<TCommand, TResult> handler, IUnitOfWork unitOfWork) 
    : ICommandHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
{
    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await handler.HandleAsync(command, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return result;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}