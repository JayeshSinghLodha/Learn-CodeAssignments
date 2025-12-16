class Program
{
    private static readonly Dictionary<string, string> CountryDatabase =
        new(StringComparer.OrdinalIgnoreCase)
        {
                { "IN", "India" },
                { "PK", "Pakistan"},
                { "US", "United States of America" },
                { "CA", "Canada" },
                { "FR", "France"},
        };

    public static void Main(string[] args)
    {
        DisplayHeader();
        RunApplicationLoop();
    }

    private static void DisplayHeader()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine(" Country code converter - Console App");
        Console.WriteLine("\n\n");
        Console.WriteLine();
    }

    private static void RunApplicationLoop()
    {
        while (true)
        {
            string userInput = GetValidCountryCode();
            if (userInput == "Q")
            {
                DisplayExitMessage();
                break;
            }

            DisplayCountryName(userInput);
            Console.WriteLine();
        }
    }

    private static string GetValidCountryCode()
    {
        while (true)
        {
            Console.Write("Enter a country code (e.g., IN, US, FR) or 'Q' to quit: ");
            string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Please enter a country code.");
                Console.WriteLine();
                continue;
            }

            if (input == "Q")
                return "Q";

            if (input.Length != 2)
            {
                Console.WriteLine("Invalid format. Please enter a 2-letter country code.");
                Console.WriteLine();
                continue;
            }

            return input;
        }
    }

    private static void DisplayCountryName(string countryCode)
    {
        if (!CountryDatabase.TryGetValue(countryCode, out var countryName))
        {
            Console.WriteLine($"Country code '{countryCode}' not found.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Country: {countryName} ({countryCode})");
    }

    private static void DisplayExitMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Exiting application!");
    }
}