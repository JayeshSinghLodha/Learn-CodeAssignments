namespace DataProcessingSystem.Interfaces;

public interface ILogger
{
    void Log(string message);
    void Flush();
}
