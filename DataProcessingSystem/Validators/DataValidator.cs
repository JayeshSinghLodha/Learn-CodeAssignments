using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem.Validators;

public class DataValidator : IDataValidator
{
    public (IReadOnlyList<DataRecord> ValidRecords, IReadOnlyList<string> Errors) Validate(IEnumerable<DataRecord> records)
    {
        var valid = new List<DataRecord>();
        var errors = new List<string>();

        foreach (var record in records)
        {
            var recordErrors = GetValidationErrors(record);

            if (recordErrors.Count == 0)
                valid.Add(record);
            else
                errors.AddRange(recordErrors);
        }

        return (valid, errors);
    }

    private static List<string> GetValidationErrors(DataRecord record)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(record.Id))
            errors.Add("Record missing ID");

        if (string.IsNullOrEmpty(record.Name))
            errors.Add($"Record {record.Id} missing name");

        return errors;
    }
}
