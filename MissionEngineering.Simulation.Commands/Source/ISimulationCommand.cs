using System.Text.Json.Serialization;

namespace MissionEngineering.Simulation;

[JsonDerivedType(typeof(SimulationSettingsCommand))]
[JsonDerivedType(typeof(MapOriginCommand))]
[JsonDerivedType(typeof(PlatformCreateCommand))]
[JsonDerivedType(typeof(PlatformDeleteCommand))]
[JsonDerivedType(typeof(PlatformAutopilotCommand))]
[JsonDerivedType(typeof(PlatformLaunchMissileCommand))]
public interface ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }
}