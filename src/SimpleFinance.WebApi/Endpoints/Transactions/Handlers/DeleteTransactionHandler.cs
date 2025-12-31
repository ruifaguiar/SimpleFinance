using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class DeleteTransactionHandler(ITransactionService transactionService, ILogger<DeleteTransactionHandler> logger)
{
    public async Task<IResult> HandleAsync(int transactionId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting transaction with ID {TransactionId}", transactionId);

        try
        {
            await transactionService.DeleteTransactionAsync(transactionId, cancellationToken);
            logger.LogInformation("Successfully deleted transaction with ID {TransactionId}", transactionId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("Transaction with ID {TransactionId} not found", transactionId);
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}

