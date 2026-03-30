namespace MissionEngineering.Core;

public class DateTimeOrigin : IDateTimeOrigin
{
    public DateTime DateTimeStart { get; set; }

    public DateTimeOrigin()
    {
    }

    public DateTime GetDateTimeFromTime(double time_s)
    {
        var dateTime = DateTimeStart.AddSeconds(time_s);

        return dateTime;
    }
}