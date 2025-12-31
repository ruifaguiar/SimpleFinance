using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.ExpenseCategories;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;

public class UpdateExpenseCategoryHandler(IExpenseCategoryService expenseCategoryService, ILogger<UpdateExpenseCategoryHandler> logger)
{
    public async Task<IResult> HandleAsync(int expenseCategoryId, ExpenseCategoryModel model, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating expense category with ID {ExpenseCategoryId}", expenseCategoryId);

        if (model.Id != null && model.Id != expenseCategoryId)
        {
            logger.LogWarning("ExpenseCategory ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", expenseCategoryId, model.Id);
            return Results.BadRequest("ExpenseCategory ID in the URL does not match the ID in the request body.");
        }

        model.Id = expenseCategoryId;
        var expenseCategory = model.ToDomain();
        Domain.DomainObjects.ExpenseCategory updatedExpenseCategory;

        try
        {
            updatedExpenseCategory = await expenseCategoryService.UpdateExpenseCategoryAsync(expenseCategory, cancellationToken);
            logger.LogInformation("Successfully updated expense category with ID {ExpenseCategoryId}", expenseCategoryId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("ExpenseCategory with ID {ExpenseCategoryId} not found", expenseCategoryId);
            return Results.NotFound();
        }

        return Results.Ok(updatedExpenseCategory.ToModel());
    }
}

