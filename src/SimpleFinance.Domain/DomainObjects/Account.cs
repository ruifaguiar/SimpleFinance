namespace SimpleFinance.Domain.DomainObjects;

public record Account
{
    public int Id { get; }
    public string Name { get; }
    public string? AccountNumber { get; }
    public int AccountTypeId { get; }
    public decimal Balance { get; }
    public string Currency { get; }
    public int InstitutionId { get; }
    public bool IsActive { get; }
    public DateOnly? OpenedAt { get; }
    public DateOnly? ClosedAt { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; }

    public Account(
        int id,
        string name,
        string? accountNumber,
        int accountTypeId,
        decimal balance,
        string currency,
        int institutionId,
        bool isActive,
        DateOnly? openedAt,
        DateOnly? closedAt,
        DateTime createdAt,
        DateTime? modifiedAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        }

        if (balance < 0)
        {
            throw new ArgumentException("Balance cannot be negative", nameof(balance));
        }

        Id = id;
        Name = name;
        AccountNumber = accountNumber;
        AccountTypeId = accountTypeId;
        Balance = balance;
        Currency = currency;
        InstitutionId = institutionId;
        IsActive = isActive;
        OpenedAt = openedAt;
        ClosedAt = closedAt;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
    }
}
