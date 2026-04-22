using DataProcessingSystem.Interfaces;

namespace DataProcessingSystem.Logging;

public class FileLogger : ILogger
{
    private readonly string _logFilePath;
    private readonly System.Text.StringBuilder _buffer = new();

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _buffer.AppendLine($"[{timestamp}] {message}");
    }

    public void Flush()
    {
        File.WriteAllText(_logFilePath, _buffer.ToString());
    }
}
