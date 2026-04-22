using DataProcessingSystem.Interfaces;
using DataProcessingSystem.Models;

namespace DataProcessingSystem;

public class DataProcessor
{
    private readonly IDataReader _reader;
    private readonly IDataParser _parser;
    private readonly IDataValidator _validator;
    private readonly IDataTransformer _transformer;
    private readonly IStatisticsCalculator _statisticsCalculator;
    private readonly IDataExporter _primaryExporter;
    private readonly ILogger _logger;
    private readonly ProcessingConfiguration _config;

    private readonly Dictionary<string, IDataExporter> _exporters;

    private List<DataRecord> _processedRecords = [];

    public DataProcessor(
        string inputFilePath,
        string outputFilePath,
        IDataReader reader,
        IDataParser parser,
        IDataValidator validator,
        IDataTransformer transformer,
        IStatisticsCalculator statisticsCalculator,
        IDataExporter primaryExporter,
        IEnumerable<IDataExporter> allExporters,
        ILogger logger,
        ProcessingConfiguration config)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
        _reader = reader;
        _parser = parser;
        _validator = validator;
        _transformer = transformer;
        _statisticsCalculator = statisticsCalculator;
        _primaryExporter = primaryExporter;
        _logger = logger;
        _config = config;
        _exporters = allExporters.ToDictionary(e => e.Format, StringComparer.OrdinalIgnoreCase);
    }

    public string InputFilePath { get; }
    public string OutputFilePath { get; }

    public ProcessingResult Process()
    {
        var allErrors = new List<string>();
        _logger.Log("Starting data processing");

        try
        {
            _logger.Log($"Reading input file: {InputFilePath}");
            var lines = _reader.ReadLines(InputFilePath).ToList();
            _logger.Log($"Read {lines.Count} lines");

            var (parsed, parseErrors) = _parser.Parse(lines);
            allErrors.AddRange(parseErrors);
            _logger.Log($"Parsed {parsed.Count} records, {parseErrors.Count} parse errors");

            IReadOnlyList<DataRecord> records = parsed;
            if (_config.ValidateData)
            {
                var (valid, validationErrors) = _validator.Validate(parsed);
                allErrors.AddRange(validationErrors);
                records = valid;
                _logger.Log($"Validation complete. {records.Count} valid records");
            }

            if (_config.TransformData)
            {
                records = _transformer.Transform(records);
                _logger.Log("Transformation complete");
            }

            _processedRecords = records.ToList();

            var stats = _statisticsCalculator.Calculate(_processedRecords, allErrors.Count);

            _logger.Log($"Writing output to: {OutputFilePath}");
            _primaryExporter.Export(_processedRecords, OutputFilePath);
            _logger.Log($"Output written. {_processedRecords.Count} records processed");

            var result = new ProcessingResult
            {
                RecordsProcessed = _processedRecords.Count,
                ErrorCount = allErrors.Count,
                ErrorMessages = allErrors,
                Statistics = stats
            };

            Console.WriteLine("Processing complete");
            Console.WriteLine($"Records processed  {result.RecordsProcessed}");
            Console.WriteLine($"Errors             {result.ErrorCount}");

            return result;
        }
        catch (Exception ex)
        {
            allErrors.Add($"Fatal error: {ex.Message}");
            _logger.Log($"FATAL ERROR: {ex.Message}");
            Console.WriteLine($"Processing failed  {ex.Message}");

            return new ProcessingResult
            {
                ErrorCount = allErrors.Count,
                ErrorMessages = allErrors
            };
        }
        finally
        {
            _logger.Flush();
        }
    }

    public void ExportByFormat(string filePath, string format)
    {
        if (!_exporters.TryGetValue(format, out var exporter))
            throw new ArgumentException($"Unsupported format: {format}");

        _logger.Log($"Exporting to {format}: {filePath}");
        exporter.Export(_processedRecords, filePath);
        _logger.Log($"{format} export complete");
        _logger.Flush();
    }

    public List<DataRecord> FilterByValue(double minValue)
    {
        var filtered = _processedRecords.Where(r => r.Value >= minValue).ToList();
        _logger.Log($"Filtered {filtered.Count} records with value >= {minValue}");
        _logger.Flush();
        return filtered;
    }

    public void DisplayStatistics(ProcessingResult result)
    {
        Console.WriteLine("\nProcessing Statistics");
        foreach (var stat in result.Statistics)
            Console.WriteLine($"  {stat.Key,-20} {stat.Value}");

        if (result.ErrorMessages.Count > 0)
        {
            Console.WriteLine("\nErrors");
            foreach (var error in result.ErrorMessages)
                Console.WriteLine($"  {error}");
        }
    }
}
