using MissionEngineering.Core;
using System.Net;

namespace MissionEngineering.Simulation;

/// <summary>
///
/// </summary>
public class Program
{
    private static string InputFolder { get; set; }

    private static string SimulationName { get; set; }

    private static string OutputFolder { get; set; }

    private static int NumberOfRuns { get; set; }

    private static bool IsGenerateSampleFiles { get; set; }

    private static string SamplesFolder { get; set; }

    private static string SimulationRunSettingsFileName { get; set; }

    private static string SimulationSettingsFileName { get; set; }

    private static string SimulationEventsFileName { get; set; }

    private static SimulationRunSettings SimulationRunSettings { get; set; }

    private static SimulationSettings SimulationSettings { get; set; }

    private static List<ISimulationEvent> SimulationEvents { get; set; }

    private static ISimulationHarness SimulationHarness { get; set; }

    /// <summary>
    /// Simulation Console Runner.
    /// </summary>
    /// <param name="inputFolder">Input Top Level Folder</param>
    /// <param name="simulationName">Simulation Name</param>
    /// <param name="outputFolder">Output Top Level Folder</param>
    /// <param name="numberOfRuns">Number Of Runs</param>
    /// <param name="isGenerateSampleFiles">Is generate sample files</param>
    /// <param name="samplesFolder">Samples Top Level Folder</param>
    public static void Main(string inputFolder, string simulationName, string outputFolder, int numberOfRuns = 1, bool isGenerateSampleFiles = false, string samplesFolder = null)
    {
        InputFolder = inputFolder;
        SimulationName = simulationName;
        OutputFolder = outputFolder;
        NumberOfRuns = numberOfRuns;
        IsGenerateSampleFiles = isGenerateSampleFiles;
        SamplesFolder = samplesFolder;

        if (IsGenerateSampleFiles)
        {
            GenerateSampleFiles();
            return;
        }

        Run();
    }

    private static void GenerateSampleFiles()
    {
        Console.WriteLine("Generating sample files...");
        Console.WriteLine();
        Console.WriteLine("    Samples Folder: " + SamplesFolder);
        Console.WriteLine();

        if (string.IsNullOrEmpty(SamplesFolder))
        {
            throw new ArgumentNullException(nameof(SamplesFolder), "Samples folder must be specified when generating sample files.");
        }

        var simulationSampleDataManager = new SimulationSampleDataManager()
        {
            SamplesFolder = SamplesFolder,
            SimulationSettings = SimulationSettingsFactory.SimulationSettings_FF_1(),
            SimulationEvents = SimulationEventFactory.FF_1(),
        };

        simulationSampleDataManager.WriteSampleData();

        Console.WriteLine("Done.");
        Console.WriteLine();
    }

    private static void Run()
    {
        GenerateInputFileNames();

        GenerateSimulationRunSettings();
        GenerateSimulationSettings();
        GenerateSimulationEvents();

        SimulationHarness = SimulationBuilder.CreateSimulationHarness();

        SimulationHarness.SimulationRunSettings = SimulationRunSettings;
        SimulationHarness.SimulationSettings = SimulationSettings;
        SimulationHarness.SimulationEvents = SimulationEvents;
        SimulationHarness.SimulationHarnessSettings.NumberOfRuns = NumberOfRuns;

        SimulationHarness.Run();
    }

    private static void GenerateInputFileNames()
    {
        var simulationName = SimulationName;

        SimulationRunSettingsFileName = GenerateFullFileName(InputFolder, SimulationName, "SimulationRunSettings.yaml");
        SimulationSettingsFileName = GenerateFullFileName(InputFolder, SimulationName, "SimulationSettings.yaml");
        SimulationEventsFileName = GenerateFullFileName(InputFolder, SimulationName, "SimulationEvents_All.yaml");
    }

    private static void GenerateSimulationRunSettings()
    {
        if (NumberOfRuns == 1)
        {
            SimulationRunSettings = SimulationRunSettingsFactory.SingleRun(InputFolder, SimulationName, OutputFolder);
        }
        else
        {
            SimulationRunSettings = SimulationRunSettingsFactory.MultipleRuns(InputFolder, SimulationName, OutputFolder);
        }
    }

    private static void GenerateSimulationSettings()
    {
        if (string.IsNullOrEmpty(SimulationSettingsFileName))
        {
            SimulationSettings = SimulationSettingsFactory.SimulationSettings_FF_1();
            return;
        }

        SimulationSettings = YamlUtilities.ReadFromYamlFile<SimulationSettings>(SimulationSettingsFileName);
    }

    private static void GenerateSimulationEvents()
    {
        if (string.IsNullOrEmpty(SimulationEventsFileName))
        {
            SimulationEvents = SimulationEventFactory.FF_1();
            return;
        }

        SimulationEvents = SimulationEventManager.ReadEventsFromFile(SimulationEventsFileName);
    }

    private static string GenerateFullFileName(string topLevelFolder, string simulationName, string fileNamePart)
    {
        var fileName = $"{simulationName}_{fileNamePart}";

        var fileNameFull = Path.Combine(topLevelFolder, simulationName);

        fileNameFull = Path.Combine(fileNameFull, fileName);

        return fileNameFull;
    }
}