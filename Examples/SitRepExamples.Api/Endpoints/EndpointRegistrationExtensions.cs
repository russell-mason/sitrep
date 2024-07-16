namespace SitRepExamples.Api.Endpoints;

public static class EndpointRegistrationExtensions
{
    public static WebApplication UseEndpointRegistration(this WebApplication app)
    {
        app.RegisterOkSanityCheck();
        app.RegisterNotFoundSanityCheck();

        return app;
    }
}
