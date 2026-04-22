using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Statistics;

public class StatisticsCalculator : IStatisticsCalculator
{
    public IReadOnlyDictionary<string, double> Calculate(IEnumerable<DataRecord> records, int errorCount)
    {
        var list = records.ToList();
        double total = list.Sum(r => r.Value);

        return new Dictionary<string, double>
        {
            ["total_records"] = list.Count,
            ["error_count"] = errorCount,
            ["total_value"] = total,
            ["average_value"] = list.Count > 0 ? total / list.Count : 0
        };
    }
}
