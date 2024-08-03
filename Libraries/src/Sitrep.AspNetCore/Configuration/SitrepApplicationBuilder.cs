namespace Sitrep.AspNetCore.Configuration;

/// <summary>
/// Allows extension methods to be chained together for configuring the sitrep application.
/// </summary>
/// <remarks>
/// Create a new instance of the SitrepApplicationBuilder class.
/// </remarks>
/// <param name="app">The application that extension methods can use for registration.</param>
public class SitrepApplicationBuilder(WebApplication app)
{
    /// <summary>
    /// Gets the application for use within extensions methods.
    /// </summary>
    public WebApplication WebApplication { get; } = app;
}