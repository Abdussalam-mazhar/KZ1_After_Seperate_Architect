using System.Net.Http.Json;
using KZ1.Monolith.Contracts;

namespace KZ1.Monolith.Clients;

public class ArchitectClient
{
    private readonly HttpClient _httpClient;

    public ArchitectClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ProcessAsync(
        MessageEnvelope message,
        bool forceFailure = false)
    {
        var request = new
        {
            Message = message,
            ForceFailure = forceFailure
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                "/architect/process",
                request);

        if (!response.IsSuccessStatusCode)
        {
            string error =
                await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"Architect failed: {error}");
        }

        var result =
            await response.Content
                .ReadFromJsonAsync<ArchitectResponse>();

        return result?.Result
            ?? throw new Exception(
                "Architect returned no result.");
    }
}


public class ArchitectResponse
{
    public string Result { get; set; } =
        string.Empty;
}