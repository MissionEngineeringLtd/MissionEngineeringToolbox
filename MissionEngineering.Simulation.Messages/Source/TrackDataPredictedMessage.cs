namespace MissionEngineering.Simulation.Messages;

public record TrackDataPredictedMessage : SimulationMessage
{
    public int TrackId { get; set; }

    public double PredictionTime { get; set; }

    public double TimeSinceLastUpdate { get; set; }

    public double LastUpdateTime { get; set; }

    public int NumberOfUpdates { get; set; }

    public int SensorPlatformId { get; set; }

    public int SensorId { get; set; }

    public string SensorType { get; set; }

    public int TargetPlatformId { get; set; }

    public int TargetReportId { get; set; }

    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }

    public double Altitude_m { get; set; }

    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double PositionDown_m { get; set; }

    public double VelocityNorth_ms { get; set; }

    public double VelocityEast_ms { get; set; }

    public double VelocityDown_ms { get; set; }

    public TrackDataPredictedMessage()
    {
        MessageType = SimulationMessageType.TrackDataPredicted;
    }
}