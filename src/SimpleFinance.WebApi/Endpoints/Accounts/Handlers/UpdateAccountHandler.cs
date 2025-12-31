using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Account;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Accounts.Handlers;

public class UpdateAccountHandler(IAccountService accountService, ILogger<UpdateAccountHandler> logger)
{
    public async Task<IResult> HandleAsync(int accountId, AccountModel model, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating account with ID {AccountId}", accountId);

        if (model.Id != null && model.Id != accountId)
        {
            logger.LogWarning("Account ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", accountId, model.Id);
            return Results.BadRequest("Account ID in the URL does not match the ID in the request body.");
        }

        model.Id = accountId;
        var account = model.ToDomain();
        Domain.DomainObjects.Account updatedAccount;

        try
        {
            updatedAccount = await accountService.UpdateAccountAsync(account, cancellationToken);
            logger.LogInformation("Successfully updated account with ID {AccountId}", accountId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("Account with ID {AccountId} not found", accountId);
            return Results.NotFound();
        }

        return Results.Ok(updatedAccount.ToModel());
    }
}

