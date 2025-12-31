using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Transaction;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class GetTransactionsByAccountIdHandler(ITransactionService transactionService)
{
    public async Task<IResult> HandleAsync(int accountId, CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetTransactionsByAccountIdAsync(accountId, cancellationToken);
        var models = transactions.Select(t => t.ToModel());
        return Results.Ok(models);
    }
}

