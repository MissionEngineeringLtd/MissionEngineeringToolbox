namespace MissionEngineering.Core;

public record SimulationTimeStamp
{
    public DateTimeOffset WallClockDateTime { get; set; }

    public DateTimeOffset SimulationDateTime { get; set; }

    public double SimulationTime_s { get; set; }

    public SimulationTimeStamp()
    {
        WallClockDateTime = DateTimeOffset.Now;
    }

    public SimulationTimeStamp(DateTimeOffset simulationDateTime, double simulationTime_s)
    {
        WallClockDateTime = DateTimeOffset.Now;

        SimulationDateTime = simulationDateTime;

        SimulationTime_s = simulationTime_s;
    }
}