namespace DataProcessingSystem.Models;

public class ProcessingResult
{
    public int RecordsProcessed { get; set; }
    public int ErrorCount { get; set; }
    public IReadOnlyList<string> ErrorMessages { get; set; } = [];
    public IReadOnlyDictionary<string, double> Statistics { get; set; } = new Dictionary<string, double>();
}
