namespace SitrepExamples.Api.DependencyInjection;

public static class ExamplesApplicationExtensions
{
    public static IApplicationBuilder UseExamples(this WebApplication app)
    {
        app.RegisterOkSanityCheck();
        app.RegisterNotFoundSanityCheck();

        return app;
    }
}