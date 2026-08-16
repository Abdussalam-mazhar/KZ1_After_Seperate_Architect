using KZ1.Monolith.Agents;
using KZ1.Monolith.Audit;
using KZ1.Monolith.Clients;


Environment.SetEnvironmentVariable(
    "SECRETARY_SECRET",
    "secretary-test-secret");

Environment.SetEnvironmentVariable(
    "JUDGE_SECRET",
    "judge-test-secret");


var auditLogger =
    new AuditLogger("C:\\Users\\786\\source\\repos\\KZ1.Architecture.Seperate\\KZ1.Monolith\\Audit\\audit.log");


var httpClient =
    new HttpClient
    {
        BaseAddress =
            new Uri("http://localhost:5001")
    };


var architectClient =
    new ArchitectClient(httpClient);


var judge =
    new Judge(auditLogger);


var secretary =
    new Secretary(
        architectClient,
        judge,
        auditLogger);


Console.WriteLine(
    "Starting KZ1...");


await secretary.RunAsync();


Console.WriteLine(
    "Finished.");