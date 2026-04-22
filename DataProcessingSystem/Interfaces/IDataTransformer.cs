using DataProcessingSystem.Models;

namespace DataProcessingSystem.Interfaces;

public interface IDataTransformer
{
    IReadOnlyList<DataRecord> Transform(IEnumerable<DataRecord> records);
}
