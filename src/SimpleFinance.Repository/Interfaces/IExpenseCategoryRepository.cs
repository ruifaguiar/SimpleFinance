using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IExpenseCategoryRepository
{
    Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken);
    Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync(CancellationToken cancellationToken);
    Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken);
    Task<bool> DeleteExpenseCategoryAsync(int id, CancellationToken cancellationToken);
}

