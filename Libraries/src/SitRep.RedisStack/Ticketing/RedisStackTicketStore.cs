namespace SitRep.RedisStack.Ticketing;

/// <summary>
/// A persistent store for tracking tickets, where items are stored using Redis Stack.
/// </summary>
public class RedisStackTicketStore(IOptions<RedisStackTicketStoreOptions> options) : ITicketStore
{
    public void Initialize()
    {
        CreateIndexes();
    }

    public async Task<Ticket?> GetTicketAsync(Guid trackingNumber)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(trackingNumber);
        var ticket = await jsonCommands.GetAsync<Ticket>(key, serializerOptions: Options.JsonSerializerOptions);

        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var searchCommands = redis.GetDatabase().FT();

        var tickets = (await searchCommands.SearchAsync(Options.TicketsIndexName, new Query($"@issuedTo:(\"{issuedTo}\")")))
                      .ToJson()
                      .Select(json => JsonSerializer.Deserialize<Ticket>(json, Options.JsonSerializerOptions)!);

        return tickets;
    }

    public async Task StoreTicketAsync(Ticket ticket)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(ticket.TrackingNumber);
        await jsonCommands.SetAsync(key, "$", ticket, serializerOptions: Options.JsonSerializerOptions);
    }

    public void DropIndexes()
    {
        try
        {
            using var redis = ConnectionMultiplexer.Connect(Options.ConnectionString);
            var searchCommands = redis.GetDatabase().FT();

            searchCommands.DropIndex(Options.TicketsIndexName);
        }
        catch (RedisServerException ex) when (ex.Message == "Unknown Index name")
        {
            // Ignore
        }
    }

    public void CreateIndexes()
    {
        try
        {
            using var redis = ConnectionMultiplexer.Connect(Options.ConnectionString);
            var searchCommands = redis.GetDatabase().FT();

            searchCommands.Create(Options.TicketsIndexName,
                                  new FTCreateParams().On(IndexDataType.JSON).Prefix(Options.TicketKeyPrefix),
                                  new Schema().AddTextField(new FieldName("$.issuedTo", "issuedTo")));
        }
        catch (RedisServerException ex) when (ex.Message == "Index already exists")
        {
            // Ignore
        }
    }

    private RedisStackTicketStoreOptions Options { get; } = options.Value;

    private string CreateKey(Guid trackingNumber) => $"{Options.TicketKeyPrefix}{trackingNumber}";
}