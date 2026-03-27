using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class SimulationData
{
    public SimulationSettings SimulationSettings { get; set; }

    public SimulationClockSettings SimulationClockSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public List<PlatformData> PlatformDataAll { get; set; }

    public List<List<PlatformData>> PlatformDataPerPlatform { get; set; }

    public SimulationData(SimulationSettings simulationSettings)
    {
        SimulationSettings = simulationSettings;
        PlatformDataAll = new List<PlatformData>();
    }
}