namespace MissionEngineering.Simulation;

public class SimulationSettings
{
    public string SimulationName { get; set; }

    public string DateTimeOrigin { get; set; }

    public double TimeStart_s { get; set; }

    public double TimeEnd_s { get; set; }

    public double TimeStep_s { get; set; }

    public double PlatformDataRecordTimeStep_s { get; set; }

    public double TrackPredictionTimeStep_s { get; set; }

    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }
}