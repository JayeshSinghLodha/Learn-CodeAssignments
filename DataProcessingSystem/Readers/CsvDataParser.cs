using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Readers;

public class CsvDataParser : IDataParser
{
    public (IReadOnlyList<DataRecord> Records, IReadOnlyList<string> Errors) Parse(IEnumerable<string> lines)
    {
        var records = new List<DataRecord>();
        var errors = new List<string>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');

            if (parts.Length < 3)
            {
                errors.Add($"Invalid line format: {line}");
                continue;
            }

            var record = new DataRecord
            {
                Id = parts[0].Trim(),
                Name = parts[1].Trim(),
            };

            if (!double.TryParse(parts[2].Trim(), out double value))
            {
                errors.Add($"Cannot parse value in line: {line}");
                continue;
            }

            record.Value = value;

            if (parts.Length >= 4 && DateTime.TryParse(parts[3].Trim(), out DateTime date))
                record.Date = date;

            records.Add(record);
        }

        return (records, errors);
    }
}
