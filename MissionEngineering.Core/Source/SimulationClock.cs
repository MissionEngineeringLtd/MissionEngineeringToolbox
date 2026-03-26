namespace MissionEngineering.Core;

public class SimulationClock : ISimulationClock
{
    public IDateTimeOrigin DateTimeOrigin { get; set; }

    public SimulationClock(IDateTimeOrigin dateTimeOrigin)
    {
        DateTimeOrigin = dateTimeOrigin;
    }

    public SimulationModelTimeStamp GetTimeStamp(double time)
    {
        var dateTime = DateTimeOrigin.DateTime.AddSeconds(time);

        var timeStamp = new SimulationModelTimeStamp(dateTime, time);

        return timeStamp;
    }
}