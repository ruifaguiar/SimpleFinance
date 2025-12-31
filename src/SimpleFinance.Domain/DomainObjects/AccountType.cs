namespace SimpleFinance.Domain.DomainObjects;

public record AccountType
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }

    public AccountType(int id, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
        Description = description;
    }
}
