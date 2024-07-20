namespace SitRepExamples.Api.Endpoints;

public static class EndpointRegistrationExtensions
{
    public static IEndpointRouteBuilder UseExampleEndpointRegistration(this IEndpointRouteBuilder builder)
    {
        builder.RegisterOkSanityCheck();
        builder.RegisterNotFoundSanityCheck();

        return builder;
    }
}
