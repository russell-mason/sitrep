namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record GetTicketStatusesResponse
{
    public GetTicketStatusesResponse(IEnumerable<TicketStatus> ticketStatuses)
    {
        TicketStatuses = ticketStatuses.Select(ticketStatus => new TicketStatusResponse(ticketStatus));
    }

    public IEnumerable<TicketStatusResponse> TicketStatuses { get; }
}
