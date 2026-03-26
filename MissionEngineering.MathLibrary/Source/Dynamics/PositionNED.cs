using CommunityToolkit.Diagnostics;

namespace MissionEngineering.MathLibrary;

public record PositionNED
{
    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double PositionDown_m { get; set; }

    public PositionNED()
    {
    }

    public PositionNED(double positionNorth_m, double positionEast_m, double positionDown_m)
    {
        PositionNorth_m = positionNorth_m;
        PositionEast_m = positionEast_m;
        PositionDown_m = positionDown_m;
    }

    public PositionNED(double[] positionNED)
    {
        Guard.IsEqualTo(positionNED.Length, 3, nameof(positionNED));

        PositionNorth_m = positionNED[0];
        PositionEast_m = positionNED[1];
        PositionDown_m = positionNED[2];
    }

    public PositionNED(Vector positionNED)
    {
        Guard.IsEqualTo(positionNED.NumberOfElements, 3, nameof(positionNED));

        PositionNorth_m = positionNED[0];
        PositionEast_m = positionNED[1];
        PositionDown_m = positionNED[2];
    }

    public static PositionNED operator +(PositionNED left, PositionNED right)
    {
        var positionNorth_m = left.PositionNorth_m + right.PositionNorth_m;
        var positionEast_m = left.PositionEast_m + right.PositionEast_m;
        var positionDown_m = left.PositionDown_m + right.PositionDown_m;

        var result = new PositionNED(positionNorth_m, positionEast_m, positionDown_m);

        return result;
    }

    public PositionLLA ToPositionLLA(PositionLLA positionLLAOrigin)
    {
        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(this, positionLLAOrigin);

        return positionLLA;
    }
}