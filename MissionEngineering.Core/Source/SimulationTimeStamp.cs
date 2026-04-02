namespace MissionEngineering.Core;

public record SimulationTimeStamp
{
    public DateTime WallClockDateTime { get; set; }

    public DateTime SimulationDateTime { get; set; }

    public double SimulationTime_s { get; set; }

    public SimulationTimeStamp()
    {
        WallClockDateTime = DateTime.Now;
    }

    public SimulationTimeStamp(DateTime simulationDateTime, double simulationTime_s)
    {
        WallClockDateTime = DateTime.Now;

        SimulationDateTime = simulationDateTime;

        SimulationTime_s = simulationTime_s;
    }
}