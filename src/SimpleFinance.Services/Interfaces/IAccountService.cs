using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface IAccountService
{
    Task<Account> AddAccountAsync(Account account, CancellationToken cancellationToken);
    Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken);
    Task<Account> GetAccountByIdAsync(int id, CancellationToken cancellationToken);
    Task<Account> UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken);
}
