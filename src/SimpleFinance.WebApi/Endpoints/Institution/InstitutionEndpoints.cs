using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.Institution.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institution;

public static class InstitutionEndpoints
{
    public static IEndpointRouteBuilder MapInstitutionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
                "/api/institution",
                ([FromServices] AddInstitutionHandler handler, [FromBody] InstitutionModel institutionModel) =>
                    handler.HandleAsync(institutionModel))
            .WithName("Add Institution");

        return endpoints;
    }
}