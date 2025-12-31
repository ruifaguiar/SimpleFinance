using SimpleFinance.WebApi.Model;
using DomainTransaction = SimpleFinance.Domain.DomainObjects.Transaction;

namespace SimpleFinance.WebApi.Mappings.Transaction;

public static class TransactionMappings
{
    public static DomainTransaction ToDomain(this TransactionModel model)
    {
        return new DomainTransaction(
            model.Id ?? 0,
            model.AccountId,
            model.Amount,
            model.TransactionTypeId,
            model.TransactionDate,
            model.Description,
            model.BalanceAfterTransaction,
            model.Currency,
            model.CreatedAt ?? DateTime.UtcNow,
            model.UpdatedAt);
    }

    public static TransactionModel ToModel(this DomainTransaction transaction)
    {
        return new TransactionModel
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            TransactionTypeId = transaction.TransactionTypeId,
            TransactionDate = transaction.TransactionDate,
            Description = transaction.Description,
            BalanceAfterTransaction = transaction.BalanceAfterTransaction,
            Currency = transaction.Currency,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt
        };
    }
}

