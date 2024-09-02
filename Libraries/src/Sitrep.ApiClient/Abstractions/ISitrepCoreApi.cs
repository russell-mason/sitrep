namespace Sitrep.ApiClient.Abstractions;

/// <summary>
/// SITREP Core API client.
/// <para>
/// This allows simplified .NET access to the SITREP HTTP based API, and is designed to be used from the
/// perspective of individual users allowing them to access only tickets that were issued to them.
/// </para>
/// </summary>
public interface ISitrepCoreApi
{
    /// <summary>
    /// Requests a single ticket with the specific tracking number.
    /// </summary>
    /// <param name="trackingNumber">The tracking number associated with the ticket.</param>
    /// <returns>The ticket if available.</returns>
    [Get("/sitrep/tickets/{trackingNumber}")]
    Task<Ticket> GetTicket(string trackingNumber);

    /// <summary>
    /// Requests all tickets based on who the ticket was originally issued to.
    /// </summary>
    /// <param name="issuedTo">An identifier that indicates who the ticket was issued to.</param>
    /// <returns>A list of tickets if available; otherwise an empty list.</returns>
    [Get("/sitrep/tickets")]
    Task<TicketList> GetTickets([Query] string issuedTo);
}
