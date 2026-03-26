using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public record PlatformState
{
    public required double Time { get; set; }

    public required int PlatformId { get; set; }

    public required PositionLLA PositionLLA { get; set; }

    public required PositionNED PositionNED { get; set; }

    public required VelocityNED VelocityNED { get; set; }
}