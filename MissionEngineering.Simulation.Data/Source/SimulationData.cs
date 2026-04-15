using MissionEngineering.Platform;
using MissionEngineering.Sensor;
using MissionEngineering.Simulation.Messages;

namespace MissionEngineering.Simulation;

public class SimulationData
{
    public SimulationSettings SimulationSettings { get; set; }

    public SimulationClockSettings SimulationClockSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public List<SimulationMessage> SimulationMessages { get; set; }

    public List<PlatformData> PlatformDataAll { get; set; }

    public List<List<PlatformData>> PlatformDataPerPlatform { get; set; }

    public List<PlatformStateRelative> PlatformDataRelativeAll { get; set; }

    public List<List<PlatformStateRelative>> PlatformDataRelativePerPlatform { get; set; }

    public List<PlatformStateMessage> PlatformStateMessagesAll { get; set; }

    public List<List<PlatformStateMessage>> PlatformStateMessagesPerPlatform { get; set; }

    public List<PlatformStateRelativeMessage> PlatformStateRelativeMessagesAll { get; set; }

    public List<List<PlatformStateRelativeMessage>> PlatformStateRelativeMessagesPerPlatform { get; set; }

    public List<SensorReport> SensorReportsAll { get; set; }

    public List<SensorReportMessage> SensorReportMessagesAll { get; set; }

    public SimulationData(SimulationSettings simulationSettings)
    {
        SimulationSettings = simulationSettings;
        SimulationMessages = [];
        PlatformDataAll = [];
        PlatformDataRelativeAll = [];
        PlatformStateMessagesAll = [];
        PlatformStateRelativeMessagesAll = [];
        SensorReportsAll = [];
        SensorReportMessagesAll = [];
    }
}