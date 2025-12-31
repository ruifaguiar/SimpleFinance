using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IAccountTypeRepository
{
    Task<AccountType> AddAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken);
    Task<IEnumerable<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken);
    Task<AccountType> GetAccountTypeByIdAsync(int id, CancellationToken cancellationToken);
    Task<AccountType> UpdateAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken);
    Task<bool> DeleteAccountTypeAsync(int id, CancellationToken cancellationToken);
}

