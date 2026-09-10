using MissionEngineering.Antenna;
using MissionEngineering.Core;
using MissionEngineering.LaTeX;
using System.ComponentModel;
using System.Security.Principal;

namespace MissionEngineering.Radar;

public class Program
{
    public static AntennaModelHarnessSettings AntennaModelHarnessSettings { get; set; }

    public static AntennaModelHarness AntennaModelHarness { get; set; }

    public static AntennaModelSettings AntennaModelSettings { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputFileName">Input file name.</param>
    /// <param name="isCreateExampleFile">If true, creates a new example input file showing the required file format.</param>
    public static void Main(string inputFileName, bool isCreateExampleFile = false)
    {
        var logFileName = inputFileName.Replace(".yaml", ".log");

        AntennaModelHarnessSettings = new AntennaModelHarnessSettings()
        {
            InputFileName = inputFileName,
            IsCreateExampleFile = isCreateExampleFile,
            LogFileName = logFileName
        };

        CreateLogger();

        AntennaModelHarness = new AntennaModelHarness()
        {
            AntennaModelHarnessSettings = AntennaModelHarnessSettings 
        };

        AntennaModelHarness.Run();

        WriteReportFile();

        LogUtilities.LogInformation($"Finished.");
    }

    private static void CreateLogger()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.CreateLogger(ahs.LogFileName);
    }

    private static void WriteReportFile()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.LogInformation($"   Writing Report Files...");

        var reportFileName = ahs.OutputFileNameCsv.Replace(".csv", "_Report.tex");
        var reportFileNamePdf = ahs.OutputFileNameCsv.Replace(".csv", "_Report.pdf");

        var inputDataTableFileNameFull = ahs.OutputFileNameCsv.Replace(".csv", "_InputDataTable.csv");

        var inputDataTableFileName = Path.GetFileName(inputDataTableFileNameFull);

        LogUtilities.LogInformation($"       {inputDataTableFileNameFull}");
        LogUtilities.LogInformation($"       {reportFileName}");
        LogUtilities.LogInformation($"       {reportFileNamePdf}");

        var outputFileName = Path.GetFileName(ahs.OutputFileNameCsv);

        var reportGenerator = new AntennaModelReportGenerator()
        {
            OutputFolder = ahs.OutputFolder,
            ReportFileNameFull = reportFileName,
            AntennaModelInputFileName = ahs.InputFileName,
            AntennaModelOutputFileName = outputFileName,
            AntennaModel = AntennaModelHarness.AntennaModel,
            InputDataTableFileName = inputDataTableFileName,
            InputDataTableFileNameFull = inputDataTableFileNameFull
        };

        reportGenerator.GenerateReport();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }
}