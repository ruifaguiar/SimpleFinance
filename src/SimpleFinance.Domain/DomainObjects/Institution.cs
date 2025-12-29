namespace SimpleFinance.Domain.DomainObjects;

public record Institution
{
    public Guid Id { get; }
    public string Name { get; }

    public Institution(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
    }
}