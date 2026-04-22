using DataProcessingSystem.Models;

namespace DataProcessingSystem.Interfaces;

public interface IDataExporter
{
    string Format { get; }
    void Export(IEnumerable<DataRecord> records, string filePath);
}
