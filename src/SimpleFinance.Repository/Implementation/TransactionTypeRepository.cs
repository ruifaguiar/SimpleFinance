using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.TransactionType;

namespace SimpleFinance.Repository.Implementation;

public class TransactionTypeRepository(SimpleFinanceDbContext dbContext) : ITransactionTypeRepository
{
    public async Task<TransactionType> AddTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken)
    {
        var dbTransactionType = transactionType.ToDb();
        dbTransactionType.CreatedAt = DateTime.UtcNow;

        dbContext.TransactionTypes.Add(dbTransactionType);
        await dbContext.SaveChangesAsync(cancellationToken);

        return dbTransactionType.ToDomain();
    }

    public async Task<IEnumerable<TransactionType>> GetAllTransactionTypesAsync(CancellationToken cancellationToken)
    {
        var transactionTypes = await dbContext.TransactionTypes.ToListAsync(cancellationToken);
        return transactionTypes.Select(t => t.ToDomain());
    }

    public async Task<TransactionType> GetTransactionTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var transactionType = await dbContext.TransactionTypes.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(transactionType, $"TransactionType with ID {id} not found.");
        return transactionType.ToDomain();
    }

    public async Task<TransactionType> UpdateTransactionTypeAsync(TransactionType transactionType, CancellationToken cancellationToken)
    {
        var dbTransactionType = await dbContext.TransactionTypes.FindAsync([transactionType.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbTransactionType, $"TransactionType with ID {transactionType.Id} not found.");

        dbTransactionType.Name = transactionType.Name;
        dbTransactionType.BalanceImpact = (Database.Entities.BalanceImpact)transactionType.BalanceImpact;
        dbTransactionType.Category = transactionType.Category;
        dbTransactionType.IsActive = transactionType.IsActive;
        dbTransactionType.Description = transactionType.Description;
        dbTransactionType.ExpenseCategoryId = transactionType.ExpenseCategoryId;
        dbTransactionType.UpdatedAt = DateTime.UtcNow;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"TransactionType with ID {transactionType.Id} was modified by another process.", ex);
        }

        return dbTransactionType.ToDomain();
    }

    public async Task<bool> DeleteTransactionTypeAsync(int id, CancellationToken cancellationToken)
    {
        var dbTransactionType = await dbContext.TransactionTypes.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbTransactionType, $"TransactionType with ID {id} not found.");

        dbContext.TransactionTypes.Remove(dbTransactionType);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}

