using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.Accounts.Handlers;

public class DeleteAccountHandler(IAccountService accountService, ILogger<DeleteAccountHandler> logger)
{
    public async Task<IResult> HandleAsync(int accountId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting account with ID {AccountId}", accountId);

        try
        {
            await accountService.DeleteAccountAsync(accountId, cancellationToken);
            logger.LogInformation("Successfully deleted account with ID {AccountId}", accountId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("Account with ID {AccountId} not found", accountId);
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}

