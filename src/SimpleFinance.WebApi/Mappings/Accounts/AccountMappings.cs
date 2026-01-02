using SimpleFinance.WebApi.Model;
using DomainAccount = SimpleFinance.Domain.DomainObjects.Account;

namespace SimpleFinance.WebApi.Mappings.Accounts;

public static class AccountMappings
{
    public static DomainAccount ToDomain(this AccountModel model)
    {
        return new DomainAccount(
            model.Id ?? 0,
            model.Name,
            model.AccountNumber,
            model.Iban,
            model.SwiftBic,
            model.AccountTypeId,
            model.Balance,
            model.Currency,
            model.InstitutionId,
            model.IsActive,
            model.OpenedAt,
            model.ClosedAt, 
            DateTime.UtcNow,
            null);
    }

    public static AccountModel ToModel(this DomainAccount account)
    {
        return new AccountModel
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            Iban = account.Iban,
            SwiftBic = account.SwiftBic,
            AccountTypeId = account.AccountTypeId,
            Balance = account.Balance,
            Currency = account.Currency,
            InstitutionId = account.InstitutionId,
            IsActive = account.IsActive,
            OpenedAt = account.OpenedAt,
            ClosedAt = account.ClosedAt
        };
    }
}

