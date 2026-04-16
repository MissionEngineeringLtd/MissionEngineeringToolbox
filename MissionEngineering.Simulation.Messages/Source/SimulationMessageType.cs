namespace MissionEngineering.Simulation.Messages;

public enum SimulationMessageType
{
    Undefined = 0,
    PlatformState = 1,
    PlatformStateRelative = 2,
    SensorReport = 3,
    TrackDataSmoothed = 4,
    TrackDataPredicted = 5,
    TrackGroup = 6,
}