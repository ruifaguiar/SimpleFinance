using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.ExpenseCategory;

namespace SimpleFinance.Repository.Implementation;

public class ExpenseCategoryRepository(SimpleFinanceDbContext dbContext) : IExpenseCategoryRepository
{
    public async Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken)
    {
        var dbExpenseCategory = expenseCategory.ToDb();

        dbContext.ExpenseCategories.Add(dbExpenseCategory);
        await dbContext.SaveChangesAsync(cancellationToken);

        return dbExpenseCategory.ToDomain();
    }

    public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync(CancellationToken cancellationToken)
    {
        var expenseCategories = await dbContext.ExpenseCategories.ToListAsync(cancellationToken);
        return expenseCategories.Select(e => e.ToDomain());
    }

    public async Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        var expenseCategory = await dbContext.ExpenseCategories.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(expenseCategory, $"ExpenseCategory with ID {id} not found.");
        return expenseCategory.ToDomain();
    }

    public async Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken)
    {
        var dbExpenseCategory = await dbContext.ExpenseCategories.FindAsync([expenseCategory.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbExpenseCategory, $"ExpenseCategory with ID {expenseCategory.Id} not found.");

        dbExpenseCategory.Name = expenseCategory.Name;
        dbExpenseCategory.Description = expenseCategory.Description;
        dbExpenseCategory.IsActive = expenseCategory.IsActive;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"ExpenseCategory with ID {expenseCategory.Id} was modified by another process.", ex);
        }

        return dbExpenseCategory.ToDomain();
    }

    public async Task<bool> DeleteExpenseCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var dbExpenseCategory = await dbContext.ExpenseCategories.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbExpenseCategory, $"ExpenseCategory with ID {id} not found.");

        dbContext.ExpenseCategories.Remove(dbExpenseCategory);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

