using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Exporters;

public class XmlExporter : IDataExporter
{
    private readonly string _dateFormat;

    public string Format => "xml";

    public XmlExporter(string dateFormat = "yyyy-MM-dd")
    {
        _dateFormat = dateFormat;
    }

    public void Export(IEnumerable<DataRecord> records, string filePath)
    {
        var lines = new List<string>
        {
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>",
            "<records>"
        };

        foreach (var r in records)
        {
            string date = r.Date.HasValue ? r.Date.Value.ToString(_dateFormat) : string.Empty;

            lines.Add("  <record>");
            lines.Add($"    <id>{r.Id}</id>");
            lines.Add($"    <name>{r.Name}</name>");
            lines.Add($"    <value>{r.Value}</value>");
            lines.Add($"    <date>{date}</date>");
            lines.Add($"    <doubled_value>{r.DoubledValue}</doubled_value>");
            lines.Add($"    <squared_value>{r.SquaredValue}</squared_value>");
            lines.Add("  </record>");
        }

        lines.Add("</records>");
        File.WriteAllLines(filePath, lines);
    }
}
