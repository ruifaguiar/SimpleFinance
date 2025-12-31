using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.TransactionTypes;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

public class AddTransactionTypeHandler(ITransactionTypeService transactionTypeService)
{
    public async Task<IResult> HandleAsync(TransactionTypeModel model, CancellationToken cancellationToken)
    {
        var transactionType = model.ToDomain();

        var savedTransactionType = await transactionTypeService.AddTransactionTypeAsync(transactionType, cancellationToken);

        return Results.CreatedAtRoute("GetTransactionTypeById", new { transactionTypeId = savedTransactionType.Id }, savedTransactionType.ToModel());
    }
}

