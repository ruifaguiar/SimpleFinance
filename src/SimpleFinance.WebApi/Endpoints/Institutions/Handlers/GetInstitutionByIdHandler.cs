using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class GetInstitutionByIdHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(Guid institutionId,CancellationToken cancellationToken)
    {
        Domain.DomainObjects.Institution institution;

        try
        {
            institution = await institutionService.GetInstitutionByIdAsync(institutionId,cancellationToken);
        }
        catch (InstitutionNotFoundException ex)
        {
            return Results.NotFound();
        }

        return Results.Ok(institution.ToModel());
    }
}