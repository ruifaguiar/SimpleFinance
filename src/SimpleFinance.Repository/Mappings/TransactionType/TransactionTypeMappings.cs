namespace SimpleFinance.Repository.Mappings.TransactionType;

using DbTransactionType = SimpleFinance.Database.Entities.TransactionType;
using DomainTransactionType = SimpleFinance.Domain.DomainObjects.TransactionType;
using DbBalanceImpact = SimpleFinance.Database.Entities.BalanceImpact;
using DomainBalanceImpact = SimpleFinance.Domain.DomainObjects.BalanceImpact;

public static class TransactionTypeMappings
{
    public static DbTransactionType ToDb(this DomainTransactionType transactionType)
    {
        return new DbTransactionType
        {
            Id = transactionType.Id,
            Name = transactionType.Name,
            BalanceImpact = (DbBalanceImpact)transactionType.BalanceImpact,
            Category = transactionType.Category,
            IsActive = transactionType.IsActive,
            Description = transactionType.Description,
            ExpenseCategoryId = transactionType.ExpenseCategoryId
        };
    }

    public static DomainTransactionType ToDomain(this DbTransactionType transactionType)
    {
        return new DomainTransactionType(
            transactionType.Id,
            transactionType.Name,
            (DomainBalanceImpact)transactionType.BalanceImpact,
            transactionType.Category,
            transactionType.IsActive,
            transactionType.Description,
            transactionType.ExpenseCategoryId);
    }
}

