using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

/// <summary>
///
/// </summary>
public class Program
{
    public static string SimulationSettingsFileName { get; set; }

    public static string ScenarioSettingsFileName { get; set; }

    public static string FlightpathDemandFileName { get; set; }

    public static int NumberOfRuns { get; set; }

    public static SimulationSettings SimulationSettings { get; set; }

    public static ScenarioSettings ScenarioSettings { get; set; }

    public static ISimulationHarness SimulationHarness { get; set; }

    /// <summary>
    /// Simulation Console Runner.
    /// </summary>
    /// <param name="simulationSettingsFileName">Simulation Settings File Name</param>
    /// <param name="scenarioSettingsFileName">Scenario Settings File Name</param>
    /// <param name="flightpathDemandFileName">Flightpath Demand File Name</param>
    /// <param name="numberOfRuns">Number Of Runs</param>
    public static void Main(string simulationSettingsFileName, string scenarioSettingsFileName, string flightpathDemandFileName, int numberOfRuns = 1)
    {
        SimulationSettingsFileName = simulationSettingsFileName;
        ScenarioSettingsFileName = scenarioSettingsFileName;
        FlightpathDemandFileName = flightpathDemandFileName;
        NumberOfRuns = numberOfRuns;

        Run();
    }

    private static void Run()
    {
        GenerateSimulationSettings();
        GenerateScenarioSettings();

        SimulationHarness = SimulationBuilder.CreateSimulationHarness();

        SimulationHarness.SimulationSettings = SimulationSettings;
        SimulationHarness.ScenarioSettings = ScenarioSettings;
        SimulationHarness.SimulationHarnessSettings.NumberOfRuns = NumberOfRuns;

        SimulationHarness.Run();
    }

    private static void GenerateSimulationSettings()
    {
        if (string.IsNullOrEmpty(SimulationSettingsFileName))
        {
            if (NumberOfRuns == 1)
            {
                SimulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Single();
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
            ScenarioSettings = ScenarioSettingsFactory.ScenarioSettings_Test_1();
            return;
        }

        ScenarioSettings = JsonUtilities.ReadFromJsonFile<ScenarioSettings>(ScenarioSettingsFileName);
    }
}