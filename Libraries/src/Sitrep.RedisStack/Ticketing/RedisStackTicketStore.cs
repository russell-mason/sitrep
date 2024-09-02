namespace Sitrep.RedisStack.Ticketing;

/// <summary>
/// A persistent store for tracking tickets, where items are stored using Redis Stack.
/// </summary>
public partial class RedisStackTicketStore(IOptions<RedisStackTicketStoreOptions> options) : ITicketStore
{
    [GeneratedRegex(@"(?<char>[!""$%^&*()_+-={}\[\]:@~;'#<>?,.|\\`])", RegexOptions.Compiled)]
    private static partial Regex EscapeRegex();

    /// <inheritdoc />
    public void Initialize()
    {
        CreateIndexes();
    }

    /// <inheritdoc />
    public virtual async Task<Ticket?> GetTicketAsync(Guid trackingNumber)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(trackingNumber);
        var ticket = await jsonCommands.GetAsync<Ticket>(key, serializerOptions: Options.JsonSerializerOptions);

        return ticket;
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var searchCommands = redis.GetDatabase().FT();

        var escaped = EscapeRegex().Replace(issuedTo, @"\${char}");

        var tickets = (await searchCommands.SearchAsync(Options.TicketsIndexName, new Query($"@issuedTo:{{{escaped}}}")))
                      .ToJson()
                      .Select(json => JsonSerializer.Deserialize<Ticket>(json, Options.JsonSerializerOptions)!);

        return tickets;
    }

    /// <inheritdoc />
    public virtual async Task StoreTicketAsync(Ticket ticket)
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(Options.ConnectionString);
        var jsonCommands = redis.GetDatabase().JSON();

        var key = CreateKey(ticket.TrackingNumber);
        await jsonCommands.SetAsync(key, "$", ticket, serializerOptions: Options.JsonSerializerOptions);
    }

    /// <summary>
    /// Drops any indexes created.
    /// </summary>
    public virtual void DropIndexes()
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
    public virtual void CreateIndexes()
    {
        try
        {
            using var redis = ConnectionMultiplexer.Connect(Options.ConnectionString);
            var searchCommands = redis.GetDatabase().FT();

            searchCommands.Create(Options.TicketsIndexName,
                                  new FTCreateParams().On(IndexDataType.JSON).Prefix(Options.TicketKeyPrefix),
                                  new Schema().AddTagField(new FieldName("$.issuedTo", "issuedTo")));
        }
        catch (RedisServerException ex) when (ex.Message == "Index already exists")
        {
            // Ignore if the index already exists
        }
    }

    /// <summary>
    /// Creates a Redis key for the ticket.
    /// </summary>
    /// <param name="trackingNumber">The tracking number to provide a unique identifier with.</param>
    /// <returns>The Redis key.</returns>
    protected virtual string CreateKey(Guid trackingNumber) => $"{Options.TicketKeyPrefix}{trackingNumber}";

    private RedisStackTicketStoreOptions Options { get; } = options.Value;
}