using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface ITransactionService
{
    Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken);
    Task<Transaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId, CancellationToken cancellationToken);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<bool> DeleteTransactionAsync(int id, CancellationToken cancellationToken);
}
