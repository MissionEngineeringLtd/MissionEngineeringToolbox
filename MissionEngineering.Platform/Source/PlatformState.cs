using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public record PlatformState
{
    public DateTime DateTime { get; set; }

    public double Time_s { get; set; }

    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

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