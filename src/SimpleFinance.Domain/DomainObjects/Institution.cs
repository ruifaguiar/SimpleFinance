namespace SimpleFinance.Domain.DomainObjects;

public record Institution
{
    public int Id { get; }
    public string Name { get; }

    public Institution(int id, string name)
    {
        if (id < 0)
        {
            throw new ArgumentException("Id cannot be less than 0", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public virtual bool Equals(Institution? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}