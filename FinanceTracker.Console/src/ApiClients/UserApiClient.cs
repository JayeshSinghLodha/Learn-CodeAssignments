using System.Net.Http.Json;
using FinanceTracker.Console.Models;

namespace FinanceTracker.Console.ApiClients;

public class UserApiClient
{
    private readonly HttpClient _http;

    public UserApiClient(HttpClient http) => _http = http;

    public async Task<UserResponse?> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _http.PostAsJsonAsync("users", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserResponse>();
    }

    public async Task<UserResponse?> GetUserAsync(Guid id)
    {
        try
        {
            return await _http.GetFromJsonAsync<UserResponse>($"users/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
