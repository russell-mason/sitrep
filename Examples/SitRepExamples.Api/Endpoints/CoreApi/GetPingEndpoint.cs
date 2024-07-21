namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class GetPingEndpoint
{
    public static void RegisterPing(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.CoreApi.Ping, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.PingName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static IResult ExecuteAsync() => Results.Ok();
}