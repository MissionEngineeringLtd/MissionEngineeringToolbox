namespace MissionEngineering.Core;

public interface IDateTimeOrigin
{
    public DateTime DateTimeStart { get; set; }

    public DateTime GetDateTimeFromTime(double time_s);
}