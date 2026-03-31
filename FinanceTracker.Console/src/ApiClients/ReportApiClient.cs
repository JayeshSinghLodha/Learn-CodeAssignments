using System.Net.Http.Json;
using FinanceTracker.Console.Models;

namespace FinanceTracker.Console.ApiClients;

public class ReportApiClient
{
    private readonly HttpClient _http;

    public ReportApiClient(HttpClient http) => _http = http;

    public async Task<MonthlySummaryResponse?> GetMonthlySummaryAsync(Guid userId, int month, int year)
    {
        return await _http.GetFromJsonAsync<MonthlySummaryResponse>(
            $"reports/summary?userId={userId}&month={month}&year={year}");
    }
}
