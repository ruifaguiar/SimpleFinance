using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.TransactionTypes;

public static class TransactionTypeEndpoints
{
    public static IEndpointRouteBuilder MapTransactionTypeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/transaction-type",
                ([FromServices] GetAllTransactionTypesHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllTransactionTypes");

        endpoints.MapPost("/api/transaction-type",
                ([FromServices] AddTransactionTypeHandler handler, [FromBody] TransactionTypeModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(model, cancellationToken))
            .WithName("AddTransactionType");

        endpoints.MapGet("/api/transaction-type/{transactionTypeId:int}",
                ([FromServices] GetTransactionTypeByIdHandler handler, [FromRoute] int transactionTypeId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionTypeId, cancellationToken))
            .WithName("GetTransactionTypeById");

        endpoints.MapPut("/api/transaction-type/{transactionTypeId:int}",
                ([FromServices] UpdateTransactionTypeHandler handler, [FromRoute] int transactionTypeId, [FromBody] TransactionTypeModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionTypeId, model, cancellationToken))
            .WithName("UpdateTransactionType");

        endpoints.MapDelete("/api/transaction-type/{transactionTypeId:int}",
                ([FromServices] DeleteTransactionTypeHandler handler, [FromRoute] int transactionTypeId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionTypeId, cancellationToken))
            .WithName("DeleteTransactionType");

        return endpoints;
    }
}

