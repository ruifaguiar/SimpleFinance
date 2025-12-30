using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;

namespace SimpleFinance.WebApi.Endpoints.Institution.Handlers;

public class GetAllInstitutionsHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync()
    {
        var institutions = await institutionService.GetAllInstitutionsAsync();
        var models = institutions.Select(i => i.ToModel());
        return Results.Ok(models);
    }
}

