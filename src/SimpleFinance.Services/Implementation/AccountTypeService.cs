using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation;

public class AccountTypeService(IAccountTypeRepository repository) : IAccountTypeService
{
    public Task<AccountType> AddAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        return repository.AddAccountTypeAsync(accountType, cancellationToken);
    }

    public Task<IEnumerable<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllAccountTypesAsync(cancellationToken);
    }

    public Task<AccountType> GetAccountTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetAccountTypeByIdAsync(id, cancellationToken);
    }

    public Task<AccountType> UpdateAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        return repository.UpdateAccountTypeAsync(accountType, cancellationToken);
    }

    public Task<bool> DeleteAccountTypeAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteAccountTypeAsync(id, cancellationToken);
    }
}

