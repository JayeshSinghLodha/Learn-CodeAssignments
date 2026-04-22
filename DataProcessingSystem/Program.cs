using DataProcessingSystem;
using DataProcessingSystem.Exporters;
using DataProcessingSystem.Logging;
using DataProcessingSystem.Models;
using DataProcessingSystem.Readers;
using DataProcessingSystem.Statistics;
using DataProcessingSystem.Transformers;
using DataProcessingSystem.Validators;

const string inputFile  = "input.csv";
const string outputFile = "output.csv";
const string dateFormat = "MM/dd/yyyy";

SampleDataGenerator.Generate(inputFile, 50);

var config = new ProcessingConfiguration
{
    ValidateData  = true,
    TransformData = true,
    DateFormat    = dateFormat,
    BatchSize     = 50,
    LogFilePath   = "processing.log"
};

var csvExporter  = new CsvExporter(dateFormat);
var jsonExporter = new JsonExporter(dateFormat);
var xmlExporter  = new XmlExporter(dateFormat);

var processor = new DataProcessor(
    inputFilePath:        inputFile,
    outputFilePath:       outputFile,
    reader:               new CsvFileReader(),
    parser:               new CsvDataParser(),
    validator:            new DataValidator(),
    transformer:          new DataTransformer(dateFormat),
    statisticsCalculator: new StatisticsCalculator(),
    primaryExporter:      csvExporter,
    allExporters:         [csvExporter, jsonExporter, xmlExporter],
    logger:               new FileLogger(config.LogFilePath),
    config:               config
);

var result = processor.Process();

processor.DisplayStatistics(result);

processor.ExportByFormat("output.json", "json");
processor.ExportByFormat("output.xml",  "xml");

try
{
    processor.ExportByFormat("output_test.json", "json");
    processor.ExportByFormat("output_test.xml",  "xml");
}
catch (Exception ex)
{
    Console.WriteLine($"Export error  {ex.Message}");
}

var filtered = processor.FilterByValue(100);
Console.WriteLine($"\nFiltered records with value 100 or above  {filtered.Count}");

Console.WriteLine($"\nRecords processed  {result.RecordsProcessed}");
Console.WriteLine($"Errors             {result.ErrorCount}");

