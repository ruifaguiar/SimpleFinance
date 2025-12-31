using SimpleFinance.WebApi.Model;
using DomainExpenseCategory = SimpleFinance.Domain.DomainObjects.ExpenseCategory;

namespace SimpleFinance.WebApi.Mappings.ExpenseCategories;

public static class ExpenseCategoryMappings
{
    public static DomainExpenseCategory ToDomain(this ExpenseCategoryModel model)
    {
        return new DomainExpenseCategory(model.Id ?? 0, model.Name, model.Description, model.IsActive);
    }

    public static ExpenseCategoryModel ToModel(this DomainExpenseCategory expenseCategory)
    {
        return new ExpenseCategoryModel
        {
            Id = expenseCategory.Id,
            Name = expenseCategory.Name,
            Description = expenseCategory.Description,
            IsActive = expenseCategory.IsActive
        };
    }
}

