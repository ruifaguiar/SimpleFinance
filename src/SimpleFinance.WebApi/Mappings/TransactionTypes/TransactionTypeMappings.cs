using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Mappings.TransactionTypes;

public static class TransactionTypeMappings
{
    public static TransactionType ToDomain(this TransactionTypeModel model)
    {
        return new TransactionType(
            model.Id ?? 0,
            model.Name,
            (BalanceImpact)model.BalanceImpact,
            model.Category,
            model.IsActive,
            model.Description,
            model.ExpenseCategoryId);
    }

    public static TransactionTypeModel ToModel(this TransactionType transactionType)
    {
        return new TransactionTypeModel
        {
            Id = transactionType.Id,
            Name = transactionType.Name,
            BalanceImpact = (int)transactionType.BalanceImpact,
            Category = transactionType.Category,
            IsActive = transactionType.IsActive,
            Description = transactionType.Description,
            ExpenseCategoryId = transactionType.ExpenseCategoryId
        };
    }
}

