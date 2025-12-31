using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Transaction;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class GetAllTransactionsHandler(ITransactionService transactionService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetAllTransactionsAsync(cancellationToken);
        var models = transactions.Select(t => t.ToModel());
        return Results.Ok(models);
    }
}

