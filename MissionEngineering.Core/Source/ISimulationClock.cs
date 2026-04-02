namespace MissionEngineering.Core;

public interface ISimulationClock
{
    IDateTimeOrigin DateTimeOrigin { get; set; }

    SimulationTimeStamp GetTimeStamp(double time);
}