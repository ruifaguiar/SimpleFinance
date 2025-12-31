using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.ExpenseCategories;

public static class ExpenseCategoryEndpoints
{
    public static IEndpointRouteBuilder MapExpenseCategoryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/expense-category",
                ([FromServices] GetAllExpenseCategoriesHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllExpenseCategories");

        endpoints.MapPost("/api/expense-category",
                ([FromServices] AddExpenseCategoryHandler handler, [FromBody] ExpenseCategoryModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(model, cancellationToken))
            .WithName("AddExpenseCategory");

        endpoints.MapGet("/api/expense-category/{expenseCategoryId:int}",
                ([FromServices] GetExpenseCategoryByIdHandler handler, [FromRoute] int expenseCategoryId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(expenseCategoryId, cancellationToken))
            .WithName("GetExpenseCategoryById");

        endpoints.MapPut("/api/expense-category/{expenseCategoryId:int}",
                ([FromServices] UpdateExpenseCategoryHandler handler, [FromRoute] int expenseCategoryId, [FromBody] ExpenseCategoryModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(expenseCategoryId, model, cancellationToken))
            .WithName("UpdateExpenseCategory");

        endpoints.MapDelete("/api/expense-category/{expenseCategoryId:int}",
                ([FromServices] DeleteExpenseCategoryHandler handler, [FromRoute] int expenseCategoryId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(expenseCategoryId, cancellationToken))
            .WithName("DeleteExpenseCategory");

        return endpoints;
    }
}

