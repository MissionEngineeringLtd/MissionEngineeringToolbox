using MissionEngineering.Math;

namespace MissionEngineering.Track;

public record TrackDataPredicted
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

    public PositionLLA PositionLLA { get; set; }

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public Attitude Attitude { get; set; }

    public PositionUncertaintyNED PositionUncertaintyNED { get; set; }

    public VelocityUncertaintyNED VelocityUncertaintyNED { get; set; }

    public AccelerationUncertaintyNED AccelerationUncertaintyNED { get; set; }
}