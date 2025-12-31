using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.TransactionTypes;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

public class GetAllTransactionTypesHandler(ITransactionTypeService transactionTypeService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var transactionTypes = await transactionTypeService.GetAllTransactionTypesAsync(cancellationToken);
        var models = transactionTypes.Select(t => t.ToModel());
        return Results.Ok(models);
    }
}

