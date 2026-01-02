using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation.Accounts;

public class TransactionService(ITransactionRepository repository) : ITransactionService
{
    public Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        return repository.AddTransactionAsync(transaction, cancellationToken);
    }

    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllTransactionsAsync(cancellationToken);
    }

    public Task<Transaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetTransactionByIdAsync(id, cancellationToken);
    }

    public Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId, CancellationToken cancellationToken)
    {
        return repository.GetTransactionsByAccountIdAsync(accountId, cancellationToken);
    }

    public Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        return repository.UpdateTransactionAsync(transaction, cancellationToken);
    }

    public Task<bool> DeleteTransactionAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteTransactionAsync(id, cancellationToken);
    }
}

