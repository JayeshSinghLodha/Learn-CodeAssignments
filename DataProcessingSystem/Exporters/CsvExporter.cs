using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Exporters;

public class CsvExporter : IDataExporter
{
    private readonly string _dateFormat;

    public string Format => "csv";

    public CsvExporter(string dateFormat = "yyyy-MM-dd")
    {
        _dateFormat = dateFormat;
    }

    public void Export(IEnumerable<DataRecord> records, string filePath)
    {
        var lines = new List<string> { "ID,NAME,VALUE,DATE,DOUBLED_VALUE,SQUARED_VALUE" };

        foreach (var record in records)
        {
            string date = record.Date.HasValue
                ? record.Date.Value.ToString(_dateFormat)
                : string.Empty;

            lines.Add($"{record.Id},{record.Name},{record.Value},{date}," +
                      $"{record.DoubledValue},{record.SquaredValue}");
        }

        File.WriteAllLines(filePath, lines);
    }
}
