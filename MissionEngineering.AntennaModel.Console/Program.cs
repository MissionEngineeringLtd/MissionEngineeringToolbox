using MissionEngineering.Antenna;
using MissionEngineering.Core;
using MissionEngineering.LaTeX;
using System.ComponentModel;
using System.Security.Principal;

namespace MissionEngineering.Radar;

public class Program
{
    public static string InputFileName { get; set; }

    public static string InputFileNameYaml { get; set; }

    public static string OutputFolder { get; set; }

    public static string OutputFileNameFull { get; set; }

    public static bool IsCreateExampleFiles { get; set; }

    public static AntennaModelSettings Settings { get; set; }

    public static AntennaModel AntennaModel { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputFileName">Input file name. Full path. Default extension is .json</param>
    /// <param name="isCreateExampleFiles">If true, creates a new example input file showing the required format.</param>
    public static void Main(string inputFileName, bool isCreateExampleFiles = false)
    {
        InputFileName = inputFileName;
        OutputFileNameFull = inputFileName.Replace(".json", ".csv");

        IsCreateExampleFiles = isCreateExampleFiles;

        CreateLogger();

        DisplaySettings();

        if (IsCreateExampleFiles)
        {
            WriteInputFile();
        }

        ReadInputFile();

        if (Settings is null)
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
        var logFileName = @"C:\Temp\MissionEngineeringToolbox\AntennaModel\AntennaModel.log";

        LogUtilities.CreateLogger(logFileName);
    }

    private static void DisplaySettings()
    {
        LogUtilities.LogInformation("AntennaModel");
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

        Settings = AntennaModelSettingsExamples.Example_1();

        InputFileNameYaml = InputFileName.Replace(".json", ".yaml");

        LogUtilities.LogInformation($"       {InputFileName}");
        LogUtilities.LogInformation($"       {InputFileNameYaml}");

        Settings.WriteToJsonFile(InputFileName);
        Settings.WriteToYamlFile(InputFileNameYaml);

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

        Settings = JsonUtilities.ReadFromJsonFile<AntennaModelSettings>(InputFileName);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void Run()
    {
        LogUtilities.LogInformation($"   Running...");

        AntennaModel = new AntennaModel()
        {
            AntennaModelSettings = Settings
        };

        AntennaModel.IsWriteData = true;

        AntennaModel.GenerateAntenna();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteOutputFile()
    {
        LogUtilities.LogInformation($"   Writing Output Files...");

        LogUtilities.LogInformation($"       {OutputFileNameFull}");

        AntennaModel.IsWriteData = true;

        AntennaModel.WriteAntennaPattern();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteReportFile()
    {
        LogUtilities.LogInformation($"   Writing Report Files...");

        var reportFileNameFull = OutputFileNameFull.Replace(".csv", "_Report.tex");
        var reportFileNameFullPdf = OutputFileNameFull.Replace(".csv", "_Report.pdf");

        var inputDataTableFileNameFull = OutputFileNameFull.Replace(".csv", "_InputDataTable.csv");

        var inputDataTableFileName = Path.GetFileName(inputDataTableFileNameFull);

        OutputFolder = Path.GetDirectoryName(OutputFileNameFull);

        LogUtilities.LogInformation($"       {inputDataTableFileNameFull}");
        LogUtilities.LogInformation($"       {reportFileNameFull}");
        LogUtilities.LogInformation($"       {reportFileNameFullPdf}");

        var outputFileName = Path.GetFileName(OutputFileNameFull);

        var reportGenerator = new AntennaModelReportGenerator()
        {
            OutputFolder = OutputFolder,
            ReportFileNameFull = reportFileNameFull,
            AntennaModelInputFileName = InputFileName,
            AntennaModelOutputFileName = outputFileName,
            AntennaModel = AntennaModel,
            InputDataTableFileName = inputDataTableFileName,
            InputDataTableFileNameFull = inputDataTableFileNameFull,
        };

        reportGenerator.GenerateReport();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }
}
