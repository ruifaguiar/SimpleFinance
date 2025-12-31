using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IAccountRepository
{
    Task<Account> AddAccountAsync(Account account, CancellationToken cancellationToken);
    Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken);
    Task<Account> GetAccountByIdAsync(int id, CancellationToken cancellationToken);
    Task<Account> UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken);
}

