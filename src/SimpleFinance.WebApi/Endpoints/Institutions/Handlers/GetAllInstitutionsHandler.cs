using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class GetAllInstitutionsHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var institutions = await institutionService.GetAllInstitutionsAsync(cancellationToken);
        var models = institutions.Select(i => i.ToModel());
        return Results.Ok(models);
    }
}

