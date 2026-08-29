using MissionEngineering.Core;
using MissionEngineering.LaTeX;

namespace MissionEngineering.Radar;

public class Program
{
    public static string InputFileName { get; set; }

    public static string InputFileNameYaml { get; set; }

    public static string OutputFolder { get; set; }

    public static string OutputFileNameFull { get; set; }

    public static bool IsCreateExampleFiles { get; set; }

    public static RadarDetectionModelHarnessInputs Inputs { get; set; }

    public static RadarDetectionModelHarness Harness { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputFileName">Input file name. Full path. Default extension is .json</param>
    /// <param name="outputFileName">Output file name. Full path. Default extension is .csv</param>
    /// <param name="isCreateExampleFiles">If true, creates a new example input file showing the required format.</param>
    public static void Main(string inputFileName, string outputFileName, bool isCreateExampleFiles = false)
    {
        InputFileName = inputFileName;
        OutputFileNameFull = outputFileName;
        IsCreateExampleFiles = isCreateExampleFiles;

        CreateLogger();

        DisplaySettings();

        if (IsCreateExampleFiles)
        {
            WriteInputFile();
        }

        ReadInputFile();

        if (Inputs is null)
        {
            return;
        }

        Run();

        WriteOutputFile();

        WriteReportFile();

        LogUtilities.LogInformation($"Finished.");
    }

    private static void CreateLogger()
    {
        var logFileName = @"C:\temp\MissionEngineeringToolbox\RadarDetectionModel\RadarDetectionModel.log";

        LogUtilities.CreateLogger(logFileName);
    }

    private static void DisplaySettings()
    {
        LogUtilities.LogInformation("RadarDetectionModel");
        LogUtilities.LogInformation($"");
        LogUtilities.LogInformation($"   Settings");
        LogUtilities.LogInformation($"      InputFileName        = {InputFileName}");
        LogUtilities.LogInformation($"      OutputFileName       = {OutputFileNameFull}");
        LogUtilities.LogInformation($"      IsCreateExampleFiles = {IsCreateExampleFiles}");
        LogUtilities.LogInformation($"   End Of Settings.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteInputFile()
    {
        LogUtilities.LogInformation($"   Writing Input File...");

        Inputs = RadarDetectionModelHarnessInputExamples.Example_1();

        InputFileNameYaml = InputFileName.Replace(".json", ".yaml");

        LogUtilities.LogInformation($"       {InputFileName}");
        LogUtilities.LogInformation($"       {InputFileNameYaml}");

        Inputs.WriteToJsonFile(InputFileName);
        Inputs.WriteToYamlFile(InputFileNameYaml);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void ReadInputFile()
    {
        LogUtilities.LogInformation($"   Reading Input File...");

        if (string.IsNullOrEmpty(InputFileName))
        {
            LogUtilities.LogError($"      Input file name must not be empty.");
            return;
        }

        if (!File.Exists(InputFileName))
        {
            LogUtilities.LogError($"      Input file does not exist: {InputFileName}");
            return;
        }

        Inputs = JsonUtilities.ReadFromJsonFile<RadarDetectionModelHarnessInputs>(InputFileName);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void Run()
    {
        LogUtilities.LogInformation($"   Running...");

        Harness = new RadarDetectionModelHarness()
        {
            RadarDetectionModelHarnessInputs = Inputs
        };

        Harness.Run();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteOutputFile()
    {
        LogUtilities.LogInformation($"   Writing Output Files...");

        LogUtilities.LogInformation($"       {OutputFileNameFull}");

        Harness.RadarDetectionModelData.WriteToCsvFile(OutputFileNameFull);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteReportFile()
    {
        LogUtilities.LogInformation($"   Writing Report Files...");

        var reportFileNameFull = OutputFileNameFull.Replace(".csv", "_Report.tex");

        var inputDataTableFileNameFull = OutputFileNameFull.Replace(".csv", "_InputDataTable.csv");

        var inputDataTableFileName = Path.GetFileName(inputDataTableFileNameFull);

        OutputFolder = Path.GetDirectoryName(OutputFileNameFull);

        LogUtilities.LogInformation($"       {reportFileNameFull}");
        LogUtilities.LogInformation($"       {inputDataTableFileNameFull}");

        var outputFileName = Path.GetFileName(OutputFileNameFull);

        var reportGenerator = new RadarDetectionModelReportGenerator()
        {
            OutputFolder = OutputFolder,
            ReportFileNameFull = reportFileNameFull,
            RadarDetectionModelHarnessInputFileName = InputFileName,
            RadarDetectionModelHarnessOutputFileName = outputFileName,
            RadarDetectionModelHarness = Harness,
            InputDataTableFileName = inputDataTableFileName,
            InputDataTableFileNameFull = inputDataTableFileNameFull
        };

        reportGenerator.GenerateReport();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }
}