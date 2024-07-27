namespace Sitrep.RedisStack.Configuration;

/// <summary>
/// Provides options for configuring the Redis Stack ticket store.
/// </summary>
public class RedisStackTicketStoreOptions
{
    /// <summary>
    /// Gets or sets the connection string used to connect to the Redis Stack server.
    /// </summary>
    public string ConnectionString { get; set; } = "localhost:6379";

    /// <summary>
    /// Gets or sets the name of the index used for ticket entries.
    /// </summary>
    public string TicketsIndexName { get; set; } = "idx:tickets";

    /// <summary>
    /// Gets or sets the prefix used for ticket keys.
    /// </summary>
    public string TicketKeyPrefix { get; set; } = "sitrep:tickets:";

    /// <summary>
    /// Gets or sets serialization options used when tickets are saved/loaded.
    /// <para>
    /// This is only used for the store, it has no impact of other serialization, such as
    /// ASP.NET API serialization. It does not need to match other serialization formats.
    /// </para>
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };
}