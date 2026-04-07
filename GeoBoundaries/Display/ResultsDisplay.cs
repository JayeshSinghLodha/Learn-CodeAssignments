using GeoBoundaries.Models;

namespace GeoBoundaries.Display;

public static class ResultsDisplay
{
    public static void ShowResults(IReadOnlyList<GeoLocation> locations, string query)
    {
        if (locations.Count == 0)
        {
            Console.WriteLine($"\n  No results found for \"{query}\".\n");
            return;
        }

        Console.WriteLine($"\n  {locations.Count} result(s) for \"{query}\":\n");

        for (int i = 0; i < locations.Count; i++)
        {
            var loc = locations[i];
            Console.WriteLine($"  {i + 1}. {loc.FormattedAddress}");
            Console.WriteLine($"     Latitude  : {loc.Latitude:F6}");
            Console.WriteLine($"     Longitude : {loc.Longitude:F6}");
            Console.WriteLine();
        }
    }

    public static void ShowBanner()
    {
        Console.WriteLine("  Geocoding Lookup");
        Console.WriteLine("  ----------------");
        Console.WriteLine();
    }

    public static void ShowError(string message) =>
        Console.WriteLine($"\n  Error: {message}\n");

    public static void ShowWarning(string message) =>
        Console.WriteLine($"\n  {message}\n");
}
