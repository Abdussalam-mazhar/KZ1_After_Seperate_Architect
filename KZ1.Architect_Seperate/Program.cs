var builder =
    WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls(
    "http://localhost:5001");


var app =
    builder.Build();


Directory.CreateDirectory("Data");


if (!File.Exists(
    "Data/architect-data.txt"))
{
    File.WriteAllText(
        "Data/architect-data.txt",
        "Synthetic Architect private test data.");
}


// Architect secret exists ONLY in this process

Environment.SetEnvironmentVariable(
    "ARCHITECT_SECRET",
    "architect-test-secret");


app.MapPost(
    "/architect/process",
    (ArchitectRequest request) =>
    {
        Console.WriteLine(
            $"Received request: " +
            $"{request.Message.MessageId}");

        Console.WriteLine(
            $"Correlation ID: " +
            $"{request.Message.CorrelationId}");


        if (request.ForceFailure)
        {
            Console.WriteLine(
                "Injected Architect failure.");

            return Results.Problem(
                "Injected Architect failure.");
        }


        string architectData =
            File.ReadAllText(
                "Data/architect-data.txt");


        string result =
            $"Architecture completed using: " +
            $"{architectData}";


        return Results.Ok(
            new ArchitectResponse
            {
                Result = result
            });
    });


app.MapGet(
    "/health",
    () => Results.Ok("Architect OK"));


app.Run();



public class ArchitectRequest
{
    public MessageEnvelope Message { get; set; } =
        new();

    public bool ForceFailure { get; set; }
}


public class ArchitectResponse
{
    public string Result { get; set; } =
        string.Empty;
}


public class MessageEnvelope
{
    public string MessageId { get; set; } =
        string.Empty;

    public string CorrelationId { get; set; } =
        string.Empty;

    public string Source { get; set; } =
        string.Empty;

    public string Destination { get; set; } =
        string.Empty;

    public string Action { get; set; } =
        string.Empty;

    public string Payload { get; set; } =
        string.Empty;

    public DateTime Timestamp { get; set; }
}