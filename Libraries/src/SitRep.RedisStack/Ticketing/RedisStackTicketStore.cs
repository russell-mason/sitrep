namespace SitRep.RedisStack.Ticketing;

/// <summary>
/// A persistent store for tracking tickets, where items are stored using Redis Stack.
/// </summary>
public class RedisStackTicketStore(IOptions<RedisStackTicketStoreOptions> options) : ITicketStore
{
    /// <inheritdoc />
    public void Initialize()
    {
        CreateIndexes();
    }

    /// <inheritdoc />
    public async Task<Ticket?> GetTicketAsync(Guid trackingNumber)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(trackingNumber);
        var ticket = await jsonCommands.GetAsync<Ticket>(key, serializerOptions: Options.JsonSerializerOptions);

        return ticket;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var searchCommands = redis.GetDatabase().FT();

        var tickets = (await searchCommands.SearchAsync(Options.TicketsIndexName, new Query($"@issuedTo:(\"{issuedTo}\")")))
                      .ToJson()
                      .Select(json => JsonSerializer.Deserialize<Ticket>(json, Options.JsonSerializerOptions)!);

        return tickets;
    }

    /// <inheritdoc />
    public async Task StoreTicketAsync(Ticket ticket)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(ticket.TrackingNumber);
        await jsonCommands.SetAsync(key, "$", ticket, serializerOptions: Options.JsonSerializerOptions);
    }

    /// <summary>
    /// Drops any indexes created.
    /// </summary>
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
            // Ignore if the index doesn't exist
        }
    }

    /// <summary>
    /// Creates required Redis Stack indexes.
    /// </summary>
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
            // Ignore if the index already exists
        }
    }

    private RedisStackTicketStoreOptions Options { get; } = options.Value;

    private string CreateKey(Guid trackingNumber) => $"{Options.TicketKeyPrefix}{trackingNumber}";
}