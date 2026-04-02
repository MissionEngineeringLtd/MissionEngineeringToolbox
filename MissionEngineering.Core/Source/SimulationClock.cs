namespace MissionEngineering.Core;

public class SimulationClock : ISimulationClock
{
    public IDateTimeOrigin DateTimeOrigin { get; set; }

    public SimulationClock(IDateTimeOrigin dateTimeOrigin)
    {
        DateTimeOrigin = dateTimeOrigin;
    }

    public SimulationTimeStamp GetTimeStamp(double time_s)
    {
        var dateTime = DateTimeOrigin.DateTimeStart.AddSeconds(time_s);

        var timeStamp = new SimulationTimeStamp(dateTime, time_s);

        return timeStamp;
    }
}