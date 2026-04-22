using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Exporters;

public class JsonExporter : IDataExporter
{
    private readonly string _dateFormat;

    public string Format => "json";

    public JsonExporter(string dateFormat = "yyyy-MM-dd")
    {
        _dateFormat = dateFormat;
    }

    public void Export(IEnumerable<DataRecord> records, string filePath)
    {
        var lines = new List<string> { "[" };
        var list = records.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            var r = list[i];
            string date = r.Date.HasValue ? r.Date.Value.ToString(_dateFormat) : string.Empty;

            string entry =
                $"  {{\"id\": \"{r.Id}\", \"name\": \"{r.Name}\", " +
                $"\"value\": {r.Value}, \"date\": \"{date}\", " +
                $"\"doubled_value\": {r.DoubledValue}, \"squared_value\": {r.SquaredValue}}}";

            if (i < list.Count - 1)
                entry += ",";

            lines.Add(entry);
        }

        lines.Add("]");
        File.WriteAllLines(filePath, lines);
    }
}
