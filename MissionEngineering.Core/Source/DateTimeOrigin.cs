namespace MissionEngineering.Core;

public class DateTimeOrigin : IDateTimeOrigin
{
    public DateTimeOffset DateTimeStart { get; set; }

    public DateTimeOrigin()
    {
    }

    public DateTimeOrigin(DateTimeOffset dateTimeStart)
    {
        DateTimeStart = dateTimeStart;
    }

    public DateTimeOffset GetDateTimeFromTime(double time_s)
    {
        var dateTime = DateTimeStart.AddSeconds(time_s);

        return dateTime;
    }
}