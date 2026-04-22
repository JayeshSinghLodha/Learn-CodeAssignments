namespace DataProcessingSystem.Models;

public class ProcessingConfiguration
{
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public int BatchSize { get; set; } = 100;
    public bool ValidateData { get; set; } = true;
    public bool TransformData { get; set; } = true;
    public string LogFilePath { get; set; } = "processing.log";
}
