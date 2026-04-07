using System.Text.Json.Serialization;

namespace GeoBoundaries.Models;

// Kept internal so Google's JSON shape never leaks into the domain.
internal sealed class GeocodeApiResponse
{
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("results")]
    public List<GeocodeResult> Results { get; init; } = [];
}

internal sealed class GeocodeResult
{
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; init; } = string.Empty;

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; init; } = new();
}

internal sealed class Geometry
{
    [JsonPropertyName("location")]
    public LatLng Location { get; init; } = new();
}

internal sealed class LatLng
{
    [JsonPropertyName("lat")]
    public double Lat { get; init; }

    [JsonPropertyName("lng")]
    public double Lng { get; init; }
}
