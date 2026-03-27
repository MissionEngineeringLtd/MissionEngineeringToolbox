namespace MissionEngineering.Simulation;

public record SimulationClockSettings
{
    public DateTime DateTimeOrigin { get; set; }

    public double TimeStart { get; set; }

    public double TimeEnd { get; set; }

    public double TimeStep { get; set; }
}