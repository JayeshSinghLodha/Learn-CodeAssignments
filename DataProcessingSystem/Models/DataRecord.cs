namespace DataProcessingSystem.Models;

public class DataRecord
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime? Date { get; set; }

    // Derived values computed on demand — no need to store separately
    public double DoubledValue => Value * 2;
    public double SquaredValue => Value * Value;
}
