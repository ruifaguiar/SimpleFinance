namespace SimpleFinance.WebApi.Endpoints.Institution.Handlers;

public class GetInstitutionByIdHandler
{
    public async Task<IResult> HandleAsync(Guid institutionId)
    {
        // Implementation goes here
        return Results.Ok();
    }
}