using GeoBoundaries.Models;

namespace GeoBoundaries.Interfaces;

public interface IGeocodeService
{
    Task<IReadOnlyList<GeoLocation>> GetLocationsAsync(string locationName);
}
