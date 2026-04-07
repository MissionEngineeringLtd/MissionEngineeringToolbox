namespace MissionEngineering.Core;

public class DateTimeOrigin : IDateTimeOrigin
{
    public DateTime DateTimeStart { get; set; }

    public DateTimeOrigin()
    {
    }

    public DateTimeOrigin(DateTime dateTimeStart)
    {
        DateTimeStart = dateTimeStart;
    }

    public DateTime GetDateTimeFromTime(double time_s)
    {
        var dateTime = DateTimeStart.AddSeconds(time_s);

        return dateTime;
    }
}