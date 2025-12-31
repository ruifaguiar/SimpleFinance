using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

public class DeleteAccountTypeHandler(IAccountTypeService accountTypeService, ILogger<DeleteAccountTypeHandler> logger)
{
    public async Task<IResult> HandleAsync(int accountTypeId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting account type with ID {AccountTypeId}", accountTypeId);

        try
        {
            await accountTypeService.DeleteAccountTypeAsync(accountTypeId, cancellationToken);
            logger.LogInformation("Successfully deleted account type with ID {AccountTypeId}", accountTypeId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("AccountType with ID {AccountTypeId} not found", accountTypeId);
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}

