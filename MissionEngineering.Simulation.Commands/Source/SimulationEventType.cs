namespace MissionEngineering.Simulation;

public enum SimulationEventType
{
    Undefined = 0,
    SimulationSettings,
    MapOrigin,
    PlatformCreate,
    PlatformDelete,
    PlatformAutopilot,
    PlatformLaunchMissile
}