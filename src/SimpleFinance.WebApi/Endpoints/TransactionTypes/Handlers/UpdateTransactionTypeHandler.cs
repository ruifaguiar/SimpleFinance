using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.TransactionTypes;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

public class UpdateTransactionTypeHandler(ITransactionTypeService transactionTypeService)
{
    public async Task<IResult> HandleAsync(int transactionTypeId, TransactionTypeModel model, CancellationToken cancellationToken)
    {
        var domainTransactionType = new Domain.DomainObjects.TransactionType(
            transactionTypeId,
            model.Name,
            (Domain.DomainObjects.BalanceImpact)model.BalanceImpact,
            model.Category,
            model.IsActive,
            model.Description,
            model.ExpenseCategoryId);

        var updatedTransactionType = await transactionTypeService.UpdateTransactionTypeAsync(domainTransactionType, cancellationToken);

        return Results.Ok(updatedTransactionType.ToModel());
    }
}

