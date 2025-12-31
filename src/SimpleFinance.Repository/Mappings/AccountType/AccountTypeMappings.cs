namespace SimpleFinance.Repository.Mappings.AccountType;

using DbAccountType = SimpleFinance.Database.Entities.AccountType;
using DomainAccountType = SimpleFinance.Domain.DomainObjects.AccountType;

public static class AccountTypeMappings
{
    public static DbAccountType ToDb(this DomainAccountType accountType)
    {
        return new DbAccountType
        {
            Id = accountType.Id,
            Name = accountType.Name,
            Description = accountType.Description
        };
    }

    public static DomainAccountType ToDomain(this DbAccountType accountType)
    {
        return new DomainAccountType(accountType.Id, accountType.Name, accountType.Description);
    }
}

