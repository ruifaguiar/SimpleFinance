using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.Transactions.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Transactions;

public static class TransactionEndpoints
{
    public static IEndpointRouteBuilder MapTransactionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/transaction",
                ([FromServices] GetAllTransactionsHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllTransactions");

        endpoints.MapPost("/api/transaction",
                ([FromServices] AddTransactionHandler handler, [FromBody] TransactionModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(model, cancellationToken))
            .WithName("AddTransaction");

        endpoints.MapGet("/api/transaction/{transactionId:int}",
                ([FromServices] GetTransactionByIdHandler handler, [FromRoute] int transactionId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionId, cancellationToken))
            .WithName("GetTransactionById");

        endpoints.MapGet("/api/account/{accountId:int}/transactions",
                ([FromServices] GetTransactionsByAccountIdHandler handler, [FromRoute] int accountId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountId, cancellationToken))
            .WithName("GetTransactionsByAccountId");

        endpoints.MapPut("/api/transaction/{transactionId:int}",
                ([FromServices] UpdateTransactionHandler handler, [FromRoute] int transactionId, [FromBody] TransactionModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionId, model, cancellationToken))
            .WithName("UpdateTransaction");

        endpoints.MapDelete("/api/transaction/{transactionId:int}",
                ([FromServices] DeleteTransactionHandler handler, [FromRoute] int transactionId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(transactionId, cancellationToken))
            .WithName("DeleteTransaction");

        return endpoints;
    }
}

