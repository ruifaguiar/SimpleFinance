using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes;

public static class AccountTypeEndpoints
{
    public static IEndpointRouteBuilder MapAccountTypeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/account-type",
                ([FromServices] GetAllAccountTypesHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllAccountTypes");

        endpoints.MapPost("/api/account-type",
                ([FromServices] AddAccountTypeHandler handler, [FromBody] AccountTypeModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(model, cancellationToken))
            .WithName("AddAccountType");

        endpoints.MapGet("/api/account-type/{accountTypeId:int}",
                ([FromServices] GetAccountTypeByIdHandler handler, [FromRoute] int accountTypeId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountTypeId, cancellationToken))
            .WithName("GetAccountTypeById");

        endpoints.MapPut("/api/account-type/{accountTypeId:int}",
                ([FromServices] UpdateAccountTypeHandler handler, [FromRoute] int accountTypeId, [FromBody] AccountTypeModel model, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountTypeId, model, cancellationToken))
            .WithName("UpdateAccountType");

        endpoints.MapDelete("/api/account-type/{accountTypeId:int}",
                ([FromServices] DeleteAccountTypeHandler handler, [FromRoute] int accountTypeId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(accountTypeId, cancellationToken))
            .WithName("DeleteAccountType");

        return endpoints;
    }
}

