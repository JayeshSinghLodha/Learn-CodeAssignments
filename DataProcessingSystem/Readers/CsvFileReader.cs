using DataProcessingSystem.Interfaces;

namespace DataProcessingSystem.Readers;

public class CsvFileReader : IDataReader
{
    public IEnumerable<string> ReadLines(string sourcePath)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException($"Input file not found: {sourcePath}");

        return File.ReadAllLines(sourcePath);
    }
}
