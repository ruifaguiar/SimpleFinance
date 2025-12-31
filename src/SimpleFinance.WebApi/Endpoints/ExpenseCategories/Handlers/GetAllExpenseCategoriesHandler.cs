using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.ExpenseCategories;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;

public class GetAllExpenseCategoriesHandler(IExpenseCategoryService expenseCategoryService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var expenseCategories = await expenseCategoryService.GetAllExpenseCategoriesAsync(cancellationToken);
        var models = expenseCategories.Select(e => e.ToModel());
        return Results.Ok(models);
    }
}

