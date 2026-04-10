using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public record PlatformState
{
    public SimulationTimeStamp TimeStamp { get; set; }

    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public bool IsPrediction { get; set; }

    public double LastUpdateTime_s { get; set; }
     
    public double PredictionTime_s { get; set; }

    public double PredictionTimeDelta_s { get; set; }

    public PositionLLA PositionLLA { get; set; }

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public AccelerationNED AccelerationNED { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    public Attitude Attitude { get; set; }

    public AttitudeRate AttitudeRate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDestroyed { get; set; }
}