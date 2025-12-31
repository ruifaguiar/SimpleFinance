using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.ExpenseCategories;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;

public class GetExpenseCategoryByIdHandler(IExpenseCategoryService expenseCategoryService)
{
    public async Task<IResult> HandleAsync(int expenseCategoryId, CancellationToken cancellationToken)
    {
        Domain.DomainObjects.ExpenseCategory expenseCategory;

        try
        {
            expenseCategory = await expenseCategoryService.GetExpenseCategoryByIdAsync(expenseCategoryId, cancellationToken);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.Ok(expenseCategory.ToModel());
    }
}

