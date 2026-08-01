using System.Text.Json.Serialization;

namespace MissionEngineering.Simulation;

[JsonDerivedType(typeof(SimulationSettingsEvent))]
[JsonDerivedType(typeof(MapOriginEvent))]
[JsonDerivedType(typeof(PlatformCreateEvent))]
[JsonDerivedType(typeof(PlatformDeleteEvent))]
[JsonDerivedType(typeof(PlatformAutopilotEvent))]
[JsonDerivedType(typeof(PlatformLaunchMissileEvent))]
public interface ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }
}