namespace GeoBoundaries.Models;

public class GeoLocation
{
    public string FormattedAddress { get; init; } = string.Empty;
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
