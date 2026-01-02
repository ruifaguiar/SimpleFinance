namespace SimpleFinance.Repository.Mappings.Transaction;

using DbTransaction = SimpleFinance.Database.Entities.Transaction;
using DomainTransaction = SimpleFinance.Domain.DomainObjects.Transaction;

public static class TransactionMappings
{
    public static DbTransaction ToDb(this DomainTransaction transaction)
    {
        return new DbTransaction
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            TransactionTypeId = transaction.TransactionTypeId,
            TransactionDate = DateTime.SpecifyKind(transaction.TransactionDate, DateTimeKind.Utc),
            Description = transaction.Description,
            BalanceAfterTransaction = transaction.BalanceAfterTransaction,
            Currency = transaction.Currency,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt
        };
    }

    public static DomainTransaction ToDomain(this DbTransaction transaction)
    {
        return new DomainTransaction(
            transaction.Id,
            transaction.AccountId,
            transaction.Amount,
            transaction.TransactionTypeId,
            transaction.TransactionDate,
            transaction.Description,
            transaction.BalanceAfterTransaction,
            transaction.Currency,
            transaction.CreatedAt,
            transaction.UpdatedAt);
    }
}

