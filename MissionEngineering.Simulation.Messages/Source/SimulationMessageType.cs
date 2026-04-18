namespace MissionEngineering.Simulation.Messages;

public enum SimulationMessageType
{
    Undefined = 0,
    PlatformState = 1,
    PlatformStateRelative = 2,
    ScanData = 3,
    SensorReport = 4,
    TrackDataSmoothed = 5,
    TrackDataPredicted = 6,
    TrackGroup = 7
}