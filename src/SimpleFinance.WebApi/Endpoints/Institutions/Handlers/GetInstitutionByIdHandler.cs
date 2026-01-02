using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class GetInstitutionByIdHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(int institutionId,CancellationToken cancellationToken)
    {
        Domain.DomainObjects.Institution institution;

        try
        {
            institution = await institutionService.GetInstitutionByIdAsync(institutionId,cancellationToken);
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound();
        }

        return Results.Ok(institution.ToModel());
    }
}