using DataProcessingSystem.Models;

namespace DataProcessingSystem.Interfaces;

public interface IStatisticsCalculator
{
    IReadOnlyDictionary<string, double> Calculate(IEnumerable<DataRecord> records, int errorCount);
}
