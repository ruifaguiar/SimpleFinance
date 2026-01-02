using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.Transaction;

namespace SimpleFinance.Repository.Implementation;

public class TransactionRepository(SimpleFinanceDbContext dbContext) : ITransactionRepository
{
    public async Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var dbTransaction = transaction.ToDb();
        dbTransaction.CreatedAt = DateTime.UtcNow;

        dbContext.Transactions.Add(dbTransaction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return dbTransaction.ToDomain();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions.ToListAsync(cancellationToken);
        return transactions.Select(t => t.ToDomain());
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(transaction, $"Transaction with ID {id} not found.");
        return transaction.ToDomain();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync(cancellationToken);
        return transactions.Select(t => t.ToDomain());
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var dbTransaction = await dbContext.Transactions.FindAsync([transaction.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbTransaction, $"Transaction with ID {transaction.Id} not found.");

        dbTransaction.AccountId = transaction.AccountId;
        dbTransaction.Amount = transaction.Amount;
        dbTransaction.TransactionTypeId = transaction.TransactionTypeId;
        dbTransaction.TransactionDate = DateTime.SpecifyKind(transaction.TransactionDate, DateTimeKind.Utc);
        dbTransaction.Description = transaction.Description;
        dbTransaction.BalanceAfterTransaction = transaction.BalanceAfterTransaction;
        dbTransaction.Currency = transaction.Currency;
        dbTransaction.UpdatedAt = DateTime.UtcNow;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"Transaction with ID {transaction.Id} was modified by another process.", ex);
        }

        return dbTransaction.ToDomain();
    }

    public async Task<bool> DeleteTransactionAsync(int id, CancellationToken cancellationToken)
    {
        var dbTransaction = await dbContext.Transactions.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbTransaction, $"Transaction with ID {id} not found.");

        dbContext.Transactions.Remove(dbTransaction);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

