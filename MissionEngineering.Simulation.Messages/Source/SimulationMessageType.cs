namespace MissionEngineering.Simulation.Messages;

public enum SimulationMessageType
{
    Undefined = 0,
    PlatformState = 1,
    PlatformStateRelative = 2,
    SensorReport = 3,
    TrackUpdate = 4,
    TrackPredict = 5,
    TrackGroup = 6,
}