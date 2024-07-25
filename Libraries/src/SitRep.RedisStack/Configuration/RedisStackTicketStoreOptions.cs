namespace SitRep.RedisStack.Configuration;

public class RedisStackTicketStoreOptions
{
    public string ConnectionString { get; set; } = "localhost:6379";

    public string TicketsIndexName { get; set; } = "idx:tickets";

    public string TicketKeyPrefix { get; set; } = "sitrep:tickets:";

    public JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };
}