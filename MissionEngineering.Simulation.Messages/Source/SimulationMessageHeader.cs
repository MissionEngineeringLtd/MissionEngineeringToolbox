namespace MissionEngineering.Simulation.Messages;

public record SimulationMessageHeader
{
    public int SourceId { get; set; }

    public string SourceName { get; set; }

    public int MessageId { get; set; }

    public string MessageDescription { get; set; }

    public DateTime WallClockDateTime { get; set; }

    public DateTime SimulationDateTime { get; set; }

    public double SimulationTime_s { get; set; }
}