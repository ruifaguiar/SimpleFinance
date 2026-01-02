namespace SimpleFinance.Domain.DomainObjects;

public record Transaction
{
    public int Id { get; }
    public int AccountId { get; }
    public decimal Amount { get; }
    public int TransactionTypeId { get; }
    public DateTime TransactionDate { get; }
    public string? Description { get; }
    public decimal BalanceAfterTransaction { get; }
    public string Currency { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    public Transaction(
        int id,
        int accountId,
        decimal amount,
        int transactionTypeId,
        DateTime transactionDate,
        string? description,
        decimal balanceAfterTransaction,
        string currency,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException("AccountId must be greater than zero", nameof(accountId));
        }

        if (transactionTypeId <= 0)
        {
            throw new ArgumentException("TransactionTypeId must be greater than zero", nameof(transactionTypeId));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        }

        if (currency.Length != 3)
        {
            throw new ArgumentException("Currency must be a 3-character code", nameof(currency));
        }
        
        Id = id;
        AccountId = accountId;
        Amount = amount;
        TransactionTypeId = transactionTypeId;
        TransactionDate = transactionDate;
        Description = description;
        BalanceAfterTransaction = balanceAfterTransaction;
        Currency = currency;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
