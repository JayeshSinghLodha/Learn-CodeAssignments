namespace GeoBoundaries.Interfaces;

public interface ILocationValidator
{
    bool IsValid(string? input, out string errorMessage);
}
