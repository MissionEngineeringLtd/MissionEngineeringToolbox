using System.Text.Json.Serialization;

namespace MissionEngineering.Simulation;

[JsonDerivedType(typeof(PlatformCreateEvent))]
[JsonDerivedType(typeof(PlatformDeleteEvent))]
[JsonDerivedType(typeof(PlatformAutopilotEvent))]
[JsonDerivedType(typeof(PlatformLaunchMissileEvent))]
[JsonDerivedType(typeof(ZoneCreateEvent))]
public interface ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }
}