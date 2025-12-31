using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation;

public class AccountService(IAccountRepository repository) : IAccountService
{
    public Task<Account> AddAccountAsync(Account account, CancellationToken cancellationToken)
    {
        return repository.AddAccountAsync(account, cancellationToken);
    }

    public Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllAccountsAsync(cancellationToken);
    }

    public Task<Account> GetAccountByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetAccountByIdAsync(id, cancellationToken);
    }

    public Task<Account> UpdateAccountAsync(Account account, CancellationToken cancellationToken)
    {
        return repository.UpdateAccountAsync(account, cancellationToken);
    }

    public Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteAccountAsync(id, cancellationToken);
    }
}

