using System.Text.Json;
using GeoBoundaries.Interfaces;
using GeoBoundaries.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GeoBoundaries.Services;

public sealed class GoogleGeocodeService : IGeocodeService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _geocodeUrl;
    private readonly ILogger<GoogleGeocodeService> _logger;

    public GoogleGeocodeService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<GoogleGeocodeService> logger)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Google:ApiKey"]
            ?? throw new InvalidOperationException("'Google:ApiKey' is not configured.");
        _geocodeUrl = configuration["Google:GeocodeUrl"]
            ?? throw new InvalidOperationException("'Google:GeocodeUrl' is not configured.");
        _logger = logger;
    }

    public async Task<IReadOnlyList<GeoLocation>> GetLocationsAsync(string locationName)
    {
        var url = $"{_geocodeUrl}?address={Uri.EscapeDataString(locationName)}&key={_apiKey}";

        _logger.LogInformation("Querying geocoding API for: {LocationName}", locationName);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<GeocodeApiResponse>(json)
            ?? throw new InvalidOperationException("Received an empty response from the geocoding API.");

        return MapToLocations(apiResponse, locationName);
    }

    private IReadOnlyList<GeoLocation> MapToLocations(GeocodeApiResponse apiResponse, string locationName)
    {
        switch (apiResponse.Status)
        {
            case "OK":
                _logger.LogInformation("Received {Count} result(s) for: {LocationName}", apiResponse.Results.Count, locationName);
                return apiResponse.Results
                    .Select(r => new GeoLocation
                    {
                        FormattedAddress = r.FormattedAddress,
                        Latitude = r.Geometry.Location.Lat,
                        Longitude = r.Geometry.Location.Lng
                    })
                    .ToList()
                    .AsReadOnly();

            case "ZERO_RESULTS":
                _logger.LogWarning("No results found for: {LocationName}", locationName);
                return [];

            case "REQUEST_DENIED":
                throw new InvalidOperationException("API request denied. Verify your API key and that the Geocoding API is enabled.");

            case "OVER_DAILY_LIMIT":
            case "OVER_QUERY_LIMIT":
                throw new InvalidOperationException("API quota exceeded.");

            case "INVALID_REQUEST":
                throw new InvalidOperationException("Invalid request sent to geocoding API.");

            default:
                throw new InvalidOperationException($"Unexpected API status: '{apiResponse.Status}'.");
        }
    }
}
