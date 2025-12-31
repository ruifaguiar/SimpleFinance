using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation;

public class ExpenseCategoryService(IExpenseCategoryRepository repository) : IExpenseCategoryService
{
    public Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken)
    {
        return repository.AddExpenseCategoryAsync(expenseCategory, cancellationToken);
    }

    public Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategoriesAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllExpenseCategoriesAsync(cancellationToken);
    }

    public Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetExpenseCategoryByIdAsync(id, cancellationToken);
    }

    public Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory, CancellationToken cancellationToken)
    {
        return repository.UpdateExpenseCategoryAsync(expenseCategory, cancellationToken);
    }

    public Task<bool> DeleteExpenseCategoryAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteExpenseCategoryAsync(id, cancellationToken);
    }
}

