namespace KZ1.Monolith.Contracts;

public class MessageEnvelope
{
    public string MessageId { get; set; } =
        Guid.NewGuid().ToString();

    public string CorrelationId { get; set; } =
        Guid.NewGuid().ToString();

    public string Source { get; set; } = string.Empty;

    public string Destination { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } =
        DateTime.UtcNow;
}