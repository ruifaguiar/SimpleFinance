using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface ITransactionTypeService
{
    Task<TransactionType> AddTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionType>> GetAllTransactionTypesAsync(CancellationToken cancellationToken);
    Task<TransactionType> GetTransactionTypeByIdAsync(int id, CancellationToken cancellationToken);
    Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken);
    Task<bool> DeleteTransactionTypeAsync(int id, CancellationToken cancellationToken);
}

