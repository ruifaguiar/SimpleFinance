using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface IExpenseCategoryService
{
    Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken);
    Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync(CancellationToken cancellationToken);
    Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken);
    Task<bool> DeleteExpenseCategoryAsync(int id, CancellationToken cancellationToken);
}

