using DataProcessingSystem.Models;

namespace DataProcessingSystem.Interfaces;

public interface IDataParser
{
    (IReadOnlyList<DataRecord> Records, IReadOnlyList<string> Errors) Parse(IEnumerable<string> lines);
}
