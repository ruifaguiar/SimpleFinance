namespace SimpleFinance.Domain.DomainObjects;

public record TransactionType
{
    public int Id { get; }
    public string Name { get; }
    public BalanceImpact BalanceImpact { get; }
    public string? Category { get; }
    public bool IsActive { get; }
    public string? Description { get; }
    public int ExpenseCategoryId { get; }

    public TransactionType(
        int id,
        string name,
        BalanceImpact balanceImpact,
        string? category = null,
        bool isActive = true,
        string? description = null,
        int expenseCategoryId = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
        BalanceImpact = balanceImpact;
        Category = category;
        IsActive = isActive;
        Description = description;
        ExpenseCategoryId = expenseCategoryId;
    }
}

