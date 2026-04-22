namespace DataProcessingSystem;

public static class SampleDataGenerator
{
    public static void Generate(string filePath, int recordCount)
    {
        var random = new Random();
        var lines = Enumerable.Range(1, recordCount).Select(i =>
        {
            string id = $"ID{i:D4}";
            string name = $"Item{i}";
            double value = random.Next(10, 1000);
            DateTime date = DateTime.Now.AddDays(-random.Next(0, 365));
            return $"{id},{name},{value},{date:yyyy-MM-dd}";
        });

        File.WriteAllLines(filePath, lines);
        Console.WriteLine($"Generated {recordCount} sample records in {filePath}");
    }
}
