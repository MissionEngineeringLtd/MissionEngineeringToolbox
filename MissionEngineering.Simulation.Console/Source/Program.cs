using MissionEngineering.Core;
using System.Net;

namespace MissionEngineering.Simulation;

/// <summary>
///
/// </summary>
public class Program
{
    private static string InputFolder { get; set; }

    private static string OutputFolder { get; set; }

    private static int NumberOfRuns { get; set; }

    private static bool IsGenerateSampleFiles { get; set; }

    private static string SamplesFolder { get; set; }

    private static string SimulationSettingsFileName { get; set; }

    private static string ScenarioSettingsFileName { get; set; }

    private static string SimulationEventsFileName { get; set; }

    private static SimulationSettings SimulationSettings { get; set; }

    private static ScenarioSettings ScenarioSettings { get; set; }

    private static List<ISimulationEvent> SimulationEvents { get; set; }

    private static ISimulationHarness SimulationHarness { get; set; }

    /// <summary>
    /// Simulation Console Runner.
    /// </summary>
    /// <param name="inputFolder">Input Folder</param>
    /// <param name="outputFolder">Output Folder</param>
    /// <param name="numberOfRuns">Number Of Runs</param>
    /// <param name="isGenerateSampleFiles">Switch to generate sample files</param>
    /// <param name="samplesFolder">Samples Folder</param>
    public static void Main(string inputFolder, string outputFolder, int numberOfRuns = 1, bool isGenerateSampleFiles = false, string samplesFolder = null)
    {
        InputFolder = inputFolder;
        OutputFolder = outputFolder;
        NumberOfRuns = numberOfRuns;
        IsGenerateSampleFiles = isGenerateSampleFiles;
        SamplesFolder = samplesFolder;

        if (IsGenerateSampleFiles )
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
            SimulationSettings = SimulationSettingsFactory.SimulationSettings_FF_1_Single(),
            ScenarioSettings = ScenarioSettingsFactory.ScenarioSettings_FF_1(),
            SimulationEvents = SimulationEventFactory.FF_1(),
        };

        simulationSampleDataManager.WriteSampleData();

        Console.WriteLine("Done.");
        Console.WriteLine();
    }

    private static void Run()
    {
        GenerateSimulationSettings();
        GenerateScenarioSettings();
        GenerateSimulationEvents();

        SimulationHarness = SimulationBuilder.CreateSimulationHarness();

        SimulationHarness.SimulationSettings = SimulationSettings;
        SimulationHarness.ScenarioSettings = ScenarioSettings;
        SimulationHarness.SimulationEvents = SimulationEvents;
        SimulationHarness.SimulationHarnessSettings.NumberOfRuns = NumberOfRuns;

        SimulationHarness.Run();
    }

    private static void GenerateSimulationSettings()
    {
        if (string.IsNullOrEmpty(SimulationSettingsFileName))
        {
            if (NumberOfRuns == 1)
            {
                SimulationSettings = SimulationSettingsFactory.SimulationSettings_FF_1_Single();
            }
            else
            {
                SimulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Multiple();
            }

            return;
        }

        SimulationSettings = JsonUtilities.ReadFromJsonFile<SimulationSettings>(SimulationSettingsFileName);
    }

    private static void GenerateScenarioSettings()
    {
        if (string.IsNullOrEmpty(ScenarioSettingsFileName))
        {
            ScenarioSettings = ScenarioSettingsFactory.ScenarioSettings_FF_1();
            return;
        }

        ScenarioSettings = JsonUtilities.ReadFromJsonFile<ScenarioSettings>(ScenarioSettingsFileName);
    }

    private static void GenerateSimulationEvents()
    {
        if (string.IsNullOrEmpty(SimulationEventsFileName))
        {
            SimulationEvents = SimulationEventFactory.FF_1();
            return;
        }

        SimulationEvents = JsonUtilities.ReadFromJsonFile<List<ISimulationEvent>>(SimulationEventsFileName);
    }
}