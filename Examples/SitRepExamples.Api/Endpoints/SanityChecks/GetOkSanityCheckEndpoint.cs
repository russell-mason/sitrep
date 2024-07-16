namespace SitRepExamples.Api.Endpoints.SanityChecks;

public static class GetOkSanityCheckEndpoint
{
    public static void RegisterOkSanityCheck(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.SanityChecks.Ok, Execute)
                       .WithOpenApi()
                       .WithName("GetOkSanityCheck")
                       .WithTags("Sanity Checks");
    }

    private static object Execute() => Results.Ok(new { Response = "Success - OK" });
}
