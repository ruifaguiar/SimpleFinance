namespace SimpleFinance.Repository.Mappings.ExpenseCategory;

using DbExpenseCategory = SimpleFinance.Database.Entities.ExpenseCategory;
using DomainExpenseCategory = SimpleFinance.Domain.DomainObjects.ExpenseCategory;

public static class ExpenseCategoryMappings
{
    public static DbExpenseCategory ToDb(this DomainExpenseCategory expenseCategory)
    {
        return new DbExpenseCategory
        {
            Id = expenseCategory.Id,
            Name = expenseCategory.Name,
            Description = expenseCategory.Description,
            IsActive = expenseCategory.IsActive
        };
    }

    public static DomainExpenseCategory ToDomain(this DbExpenseCategory expenseCategory)
    {
        return new DomainExpenseCategory(
            expenseCategory.Id,
            expenseCategory.Name,
            expenseCategory.Description,
            expenseCategory.IsActive);
    }
}

