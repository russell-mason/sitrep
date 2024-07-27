namespace Sitrep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to check if the service is running.
/// </summary>
public static class GetPingEndpoint
{
    /// <summary>
    /// Registers the Ping endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterPing(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.Ping, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.PingName)
                       .WithTags(Routes.Tag);
    }

    private static IResult ExecuteAsync() => Results.Ok();
}