using SimpleFinance.WebApi.Model;
using DomainAccountType = SimpleFinance.Domain.DomainObjects.AccountType;

namespace SimpleFinance.WebApi.Mappings.Accounts;

public static class AccountTypeMappings
{
    public static DomainAccountType ToDomain(this AccountTypeModel model)
    {
        return new DomainAccountType(model.Id ?? 0, model.Name, model.Description);
    }

    public static AccountTypeModel ToModel(this DomainAccountType accountType)
    {
        return new AccountTypeModel
        {
            Id = accountType.Id,
            Name = accountType.Name,
            Description = accountType.Description
        };
    }
}

