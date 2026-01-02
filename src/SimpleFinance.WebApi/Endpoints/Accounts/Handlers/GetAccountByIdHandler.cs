using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;

namespace SimpleFinance.WebApi.Endpoints.Accounts.Handlers;

public class GetAccountByIdHandler(IAccountService accountService)
{
    public async Task<IResult> HandleAsync(int accountId, CancellationToken cancellationToken)
    {
        Domain.DomainObjects.Account account;

        try
        {
            account = await accountService.GetAccountByIdAsync(accountId, cancellationToken);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.Ok(account.ToModel());
    }
}

