using KZ1.Monolith.Audit;
using KZ1.Monolith.Clients;
using KZ1.Monolith.Contracts;

namespace KZ1.Monolith.Agents;

public class Secretary
{
    private readonly ArchitectClient _architect;
    private readonly Judge _judge;
    private readonly AuditLogger _auditLogger;


    public Secretary() { }
    public Secretary(
        ArchitectClient architect,
        Judge judge,
        AuditLogger auditLogger)
    {
        _architect = architect;
        _judge = judge;
        _auditLogger = auditLogger;
    }
    public string? TryReadArchitectSecret()
    {
        return Environment.GetEnvironmentVariable(
            "ARCHITECT_SECRET");
    }

    public string? TryReadJudgeSecret()
    {
        return Environment.GetEnvironmentVariable(
            "JUDGE_SECRETS");
    }

    public async Task RunAsync(
        bool forceArchitectFailure = false)
    {
        var message = new MessageEnvelope
        {
            Source = "Secretary",
            Destination = "Architect",
            Action = "CreateArchitecture",
            Payload = "Synthetic test request"
        };

        _auditLogger.Log(
            message,
            "SENT");

        try
        {
            string result =
                await _architect.ProcessAsync(
                    message,
                    forceArchitectFailure);

            string decision =
                _judge.Review(
                    result,
                    message.CorrelationId);

            Console.WriteLine(
                $"Architect: {result}");

            Console.WriteLine(
                $"Judge: {decision}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Flow failed: {ex.Message}");
        }
    }
}