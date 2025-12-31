using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.ExpenseCategories;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;

public class AddExpenseCategoryHandler(IExpenseCategoryService expenseCategoryService)
{
    public async Task<IResult> HandleAsync(ExpenseCategoryModel model, CancellationToken cancellationToken)
    {
        var expenseCategory = model.ToDomain();

        var savedExpenseCategory = await expenseCategoryService.AddExpenseCategoryAsync(expenseCategory, cancellationToken);

        return Results.CreatedAtRoute("GetExpenseCategoryById", new { expenseCategoryId = savedExpenseCategory.Id }, savedExpenseCategory.ToModel());
    }
}
