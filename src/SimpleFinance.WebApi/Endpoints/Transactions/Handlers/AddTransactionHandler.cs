using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Transaction;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class AddTransactionHandler(ITransactionService transactionService)
{
    public async Task<IResult> HandleAsync(TransactionModel model, CancellationToken cancellationToken)
    {
        var transaction = model.ToDomain();

        var savedTransaction = await transactionService.AddTransactionAsync(transaction, cancellationToken);

        return Results.CreatedAtRoute("GetTransactionById", new { transactionId = savedTransaction.Id }, savedTransaction.ToModel());
    }
}
