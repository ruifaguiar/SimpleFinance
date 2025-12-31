using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

public class DeleteTransactionTypeHandler(ITransactionTypeService transactionTypeService)
{
    public async Task<IResult> HandleAsync(int transactionTypeId, CancellationToken cancellationToken)
    {
        var result = await transactionTypeService.DeleteTransactionTypeAsync(transactionTypeId, cancellationToken);
        return result ? Results.NoContent() : Results.NotFound();
    }
}

