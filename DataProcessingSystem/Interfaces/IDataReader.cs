namespace DataProcessingSystem.Interfaces;

public interface IDataReader
{
    IEnumerable<string> ReadLines(string sourcePath);
}
