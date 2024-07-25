namespace SitRep.AspNetCore.Endpoints;

public static class GetPingEndpoint
{
    public static void RegisterPing(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.Ping, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.PingName)
                       .WithTags(Routes.Tag);
    }

    private static IResult ExecuteAsync() => Results.Ok();
}