using System.Net.Http.Json;
using FinanceTracker.Console.Models;

namespace FinanceTracker.Console.ApiClients;

public class TransactionApiClient
{
    private readonly HttpClient _http;

    public TransactionApiClient(HttpClient http) => _http = http;

    public async Task<TransactionResponse?> AddTransactionAsync(CreateTransactionRequest request)
    {
        var response = await _http.PostAsJsonAsync("transactions", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionResponse>();
    }

    public async Task<List<TransactionResponse>> GetTransactionsAsync(
        Guid userId, string? category = null, DateTime? from = null, DateTime? to = null)
    {
        var query = $"transactions?userId={userId}";
        if (!string.IsNullOrWhiteSpace(category)) query += $"&category={Uri.EscapeDataString(category)}";
        if (from.HasValue) query += $"&from={from.Value:o}";
        if (to.HasValue) query += $"&to={to.Value:o}";

        return await _http.GetFromJsonAsync<List<TransactionResponse>>(query) ?? new();
    }

    public async Task<bool> DeleteTransactionAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"transactions/{id}");
        return response.IsSuccessStatusCode;
    }
}
