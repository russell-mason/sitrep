namespace SitrepExamples.Api.Endpoints.SanityChecks;

public static class GetNotFoundSanityCheckEndpoint
{
    public static void RegisterNotFoundSanityCheck(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.SanityChecks.NotFound, Execute)
                       .WithOpenApi()
                       .WithName("GetNotFoundSanityCheck")
                       .WithTags("Sanity Checks");
    }

    private static object Execute() => Results.NotFound(new { Response = "Failure - Not Found" });
}