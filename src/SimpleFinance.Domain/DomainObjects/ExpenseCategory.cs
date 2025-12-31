namespace SimpleFinance.Domain.DomainObjects;

public record ExpenseCategory
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public bool IsActive { get; }

    public ExpenseCategory(int id, string name, string? description = null, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
    }
}
