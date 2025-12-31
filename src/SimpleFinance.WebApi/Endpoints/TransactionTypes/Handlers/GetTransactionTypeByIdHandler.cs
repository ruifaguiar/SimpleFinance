using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.TransactionTypes;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

public class GetTransactionTypeByIdHandler(ITransactionTypeService transactionTypeService)
{
    public async Task<IResult> HandleAsync(int transactionTypeId, CancellationToken cancellationToken)
    {
        var transactionType = await transactionTypeService.GetTransactionTypeByIdAsync(transactionTypeId, cancellationToken);
        return Results.Ok(transactionType.ToModel());
    }
}

