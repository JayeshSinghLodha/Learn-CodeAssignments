using DataProcessingSystem.Models;

namespace DataProcessingSystem.Interfaces;

public interface IDataValidator
{
    (IReadOnlyList<DataRecord> ValidRecords, IReadOnlyList<string> Errors) Validate(IEnumerable<DataRecord> records);
}
