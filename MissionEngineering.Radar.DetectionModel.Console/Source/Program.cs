using MissionEngineering.Core;

namespace MissionEngineering.Radar;

public class Program
{
    public static string InputFileName { get; set; }

    public static string OutputFileName { get; set; }

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
        OutputFileName = outputFileName;
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
        LogUtilities.LogInformation($"      OutputFileName       = {OutputFileName}");
        LogUtilities.LogInformation($"      IsCreateExampleFiles = {IsCreateExampleFiles}");
        LogUtilities.LogInformation($"   End Of Settings.");
        LogUtilities.LogInformation($"");
    }

    private static void WriteInputFile()
    {
        LogUtilities.LogInformation($"   Writing Input File...");

        Inputs = RadarDetectionModelHarnessInputExamples.Example_1();

        Inputs.WriteToJsonFile(InputFileName);

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
        LogUtilities.LogInformation($"   Writing Output File...");

        Harness.RadarDetectionModelData.WriteToCsvFile(OutputFileName);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }
}