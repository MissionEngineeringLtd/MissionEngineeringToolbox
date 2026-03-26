namespace MissionEngineering.Core
{
    public interface ISimulationClock
    {
        IDateTimeOrigin DateTimeOrigin { get; set; }

        SimulationModelTimeStamp GetTimeStamp(double time);
    }
}