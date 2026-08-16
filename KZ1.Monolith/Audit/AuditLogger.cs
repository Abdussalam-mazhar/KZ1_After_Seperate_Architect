using System.Text.Json;
using KZ1.Monolith.Contracts;

namespace KZ1.Monolith.Audit;

public class AuditLogger
{
    private readonly string _logFile;

    public AuditLogger(string logFile)
    {
        _logFile = logFile;
    }

    public void Log(
        MessageEnvelope message,
        string status,
        string? details = null)
    {
        var record = new
        {
            message.MessageId,
            message.CorrelationId,
            message.Source,
            message.Destination,
            message.Action,
            message.Timestamp,
            Status = status,
            Details = details
        };

        string json =
            JsonSerializer.Serialize(record);

        File.AppendAllText(
            _logFile,
            json + Environment.NewLine);
    }
}