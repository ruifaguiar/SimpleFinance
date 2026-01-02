using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class DeleteInstitutionHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(Guid institutionId, CancellationToken cancellationToken)
    {
        try
        {
            await institutionService.DeleteInstitutionAsync(institutionId, cancellationToken);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}

