using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Transformers;

public class DataTransformer : IDataTransformer
{
    private readonly string _dateFormat;

    public DataTransformer(string dateFormat = "yyyy-MM-dd")
    {
        _dateFormat = dateFormat;
    }

    public IReadOnlyList<DataRecord> Transform(IEnumerable<DataRecord> records)
    {
        var transformed = new List<DataRecord>();

        foreach (var record in records)
        {
            transformed.Add(new DataRecord
            {
                Id = record.Id,
                Name = record.Name.ToUpper(),
                Value = record.Value,
                Date = record.Date
            });
        }

        return transformed;
    }
}
