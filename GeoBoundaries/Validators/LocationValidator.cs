using GeoBoundaries.Interfaces;

namespace GeoBoundaries.Validators;

public sealed class LocationValidator : ILocationValidator
{
    private const int MaxLength = 200;

    public bool IsValid(string? input, out string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            errorMessage = "Location name cannot be empty.";
            return false;
        }

        if (input.Trim().Length > MaxLength)
        {
            errorMessage = $"Location name cannot exceed {MaxLength} characters.";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}
