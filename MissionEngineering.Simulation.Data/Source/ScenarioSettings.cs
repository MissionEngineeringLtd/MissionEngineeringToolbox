using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Sensor;

namespace MissionEngineering.Simulation;

public record ScenarioSettings
{
    public string ScenarioName { get; set; }

    public PositionLLA LLAOrigin { get; set; }

    public SimulationClockSettings SimulationClockSettings { get; set; }

    public List<PlatformSettings> PlatformSettingsList { get; set; }

    public List<SensorSettings> SensorSettingsList { get; set; }
}