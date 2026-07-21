using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Domain.Common.Abstractions;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;

public class TransactionCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
{
    private readonly IUnitWork _unitWork;
    private readonly ILogger<TransactionCommandDecorator<TCommand, TResult>> _logger;
    public TransactionCommandDecorator(
        IHandler<TCommand, TResult> handler,
        IUnitWork unitWork, 
        ILogger<TransactionCommandDecorator<TCommand, TResult>> logger) : base(handler)
    {
        _unitWork = unitWork;
        _logger = logger;
    }
    public override async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Begin transaction ...");
            
            await _unitWork.BeginAsync(cancellationToken);

            var result = await Handler.HandleAsync(command, cancellationToken);
            
            try
            {
                await _unitWork.StoreEventsAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Store events failed.");
            }
            
            await _unitWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Commit transaction.");

            return result;
        }
        catch (Exception)
        {
            await _unitWork.RollbackAsync(cancellationToken);
            _logger.LogError("Transaction failed. Rollback.");
            throw;
        }

    }
}