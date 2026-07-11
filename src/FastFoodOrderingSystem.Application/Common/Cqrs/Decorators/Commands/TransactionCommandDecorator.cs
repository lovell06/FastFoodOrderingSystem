using FastFoodOrderingSystem.Application.Abstractions.Persistence;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;

public class TransactionCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
{
    private readonly IUnitWork _unitWork;
    public TransactionCommandDecorator(IHandler<TCommand, TResult> handler,
        IUnitWork unitWork) : base(handler)
    {
        _unitWork = unitWork;
    }
    public override async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _unitWork.BeginAsync(cancellationToken);

            var result = await Handler.HandleAsync(command, cancellationToken);

            await _unitWork.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception)
        {
            await _unitWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}