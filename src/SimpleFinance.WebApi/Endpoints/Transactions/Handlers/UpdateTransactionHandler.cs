using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Transaction;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class UpdateTransactionHandler(ITransactionService transactionService, ILogger<UpdateTransactionHandler> logger)
{
    public async Task<IResult> HandleAsync(int transactionId, TransactionModel model, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating transaction with ID {TransactionId}", transactionId);

        if (model.Id != null && model.Id != transactionId)
        {
            logger.LogWarning("Transaction ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", transactionId, model.Id);
            return Results.BadRequest("Transaction ID in the URL does not match the ID in the request body.");
        }

        model.Id = transactionId;
        var transaction = model.ToDomain();
        Domain.DomainObjects.Transaction updatedTransaction;

        try
        {
            updatedTransaction = await transactionService.UpdateTransactionAsync(transaction, cancellationToken);
            logger.LogInformation("Successfully updated transaction with ID {TransactionId}", transactionId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("Transaction with ID {TransactionId} not found", transactionId);
            return Results.NotFound();
        }

        return Results.Ok(updatedTransaction.ToModel());
    }
}

