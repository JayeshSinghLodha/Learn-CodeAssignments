using System.Net.Http.Json;
using FinanceTracker.Console.Models;

namespace FinanceTracker.Console.ApiClients;

public class BudgetApiClient
{
    private readonly HttpClient _http;

    public BudgetApiClient(HttpClient http) => _http = http;

    public async Task<BudgetResponse?> SetBudgetAsync(CreateBudgetRequest request)
    {
        var response = await _http.PostAsJsonAsync("budgets", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BudgetResponse>();
    }

    public async Task<List<BudgetStatusResponse>> GetBudgetsAsync(Guid userId)
    {
        return await _http.GetFromJsonAsync<List<BudgetStatusResponse>>($"budgets?userId={userId}") ?? new();
    }
}
