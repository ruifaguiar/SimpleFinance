using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;

public class DeleteExpenseCategoryHandler(IExpenseCategoryService expenseCategoryService, ILogger<DeleteExpenseCategoryHandler> logger)
{
    public async Task<IResult> HandleAsync(int expenseCategoryId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting expense category with ID {ExpenseCategoryId}", expenseCategoryId);

        try
        {
            await expenseCategoryService.DeleteExpenseCategoryAsync(expenseCategoryId, cancellationToken);
            logger.LogInformation("Successfully deleted expense category with ID {ExpenseCategoryId}", expenseCategoryId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("ExpenseCategory with ID {ExpenseCategoryId} not found", expenseCategoryId);
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}

