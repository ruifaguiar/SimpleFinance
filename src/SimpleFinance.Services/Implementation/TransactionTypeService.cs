using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation.Accounts;

public class TransactionTypeService(ITransactionTypeRepository repository) : ITransactionTypeService
{
    public Task<TransactionType> AddTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken)
    {
        return repository.AddTransactionTypeAsync(transactionType, cancellationToken);
    }

    public Task<IEnumerable<TransactionType>> GetAllTransactionTypesAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllTransactionTypesAsync(cancellationToken);
    }

    public Task<TransactionType> GetTransactionTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetTransactionTypeByIdAsync(id, cancellationToken);
    }

    public Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken)
    {
        return repository.UpdateTransactionTypeAsync(transactionType, cancellationToken);
    }

    public Task<bool> DeleteTransactionTypeAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteTransactionTypeAsync(id, cancellationToken);
    }
}

