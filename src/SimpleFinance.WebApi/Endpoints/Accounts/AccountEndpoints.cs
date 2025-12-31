using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.Accounts.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Accounts;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/account",
                ([FromServices] GetAllAccountsHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllAccounts");

        endpoints.MapPost("/api/account",
                ([FromServices] AddAccountHandler handler, [FromBody] AccountModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(model, cancellationToken))
            .WithName("AddAccount");

        endpoints.MapGet("/api/account/{accountId:int}",
                ([FromServices] GetAccountByIdHandler handler, [FromRoute] int accountId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountId, cancellationToken))
            .WithName("GetAccountById");

        endpoints.MapPut("/api/account/{accountId:int}",
                ([FromServices] UpdateAccountHandler handler, [FromRoute] int accountId, [FromBody] AccountModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountId, model, cancellationToken))
            .WithName("UpdateAccount");

        endpoints.MapDelete("/api/account/{accountId:int}",
                ([FromServices] DeleteAccountHandler handler, [FromRoute] int accountId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountId, cancellationToken))
            .WithName("DeleteAccount");

        return endpoints;
    }
}

