namespace MissionEngineering.Core;

public interface IDateTimeOrigin
{
    public DateTimeOffset DateTimeStart { get; set; }

    public DateTimeOffset GetDateTimeFromTime(double time_s);
}