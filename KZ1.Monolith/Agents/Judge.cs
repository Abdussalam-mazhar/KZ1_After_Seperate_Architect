using KZ1.Monolith.Audit;
using KZ1.Monolith.Contracts;

namespace KZ1.Monolith.Agents;

public class Judge
{
    private readonly AuditLogger _auditLogger;

    public Judge() {
        Environment.SetEnvironmentVariable(
            "JUDGE_SECRETS",
            "Judge_123");
    }
    public Judge(AuditLogger auditLogger)
    {
        _auditLogger = auditLogger;
    }


    public string Review(
        string architectResult,
        string correlationId)
    {
        var message = new MessageEnvelope
        {
            CorrelationId = correlationId,
            Source = "Architect",
            Destination = "Judge",
            Action = "Review",
            Payload = architectResult
        };

        _auditLogger.Log(
            message,
            "SUCCESS");

        return "APPROVED";
    }
}