namespace MissionEngineering.Simulation;

public record SimulationClockSettings
{
    public string DateTimeOrigin { get; set; }

    public double TimeStart_s { get; set; }

    public double TimeEnd_s { get; set; }

    public double TimeStep_s { get; set; }

    public double TrackPredictionTimeStep_s { get; set; }
}