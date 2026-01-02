namespace SimpleFinance.Repository.Mappings.Accounts;

using DbAccount = SimpleFinance.Database.Entities.Account;
using DomainAccount = SimpleFinance.Domain.DomainObjects.Account;

public static class AccountMappings
{
    public static DbAccount ToDb(this DomainAccount account)
    {
        return new DbAccount
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
            ClosedAt = account.ClosedAt,
            CreatedAt = account.CreatedAt,
            ModifiedAt = account.ModifiedAt
        };
    }

    public static DomainAccount ToDomain(this DbAccount account)
    {
        return new DomainAccount(
            account.Id,
            account.Name,
            account.AccountNumber,
            account.Iban,
            account.SwiftBic,
            account.AccountTypeId,
            account.Balance,
            account.Currency,
            account.InstitutionId,
            account.IsActive,
            account.OpenedAt,
            account.ClosedAt,
            account.CreatedAt,
            account.ModifiedAt);
    }
}

