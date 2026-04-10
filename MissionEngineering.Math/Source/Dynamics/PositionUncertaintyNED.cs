namespace MissionEngineering.Math;

public record PositionUncertaintyNED
{
    public double PositionUncertaintyNorth_m { get; init; }

    public double PositionUncertaintyEast_m { get; init; }

    public double PositionUncertaintyDown_m { get; init; }

    public PositionUncertaintyNED()
    {
    }
    public PositionUncertaintyNED(double positionUncertaintyNorth_m, double positionUncertaintyEast_m, double positionUncertaintyDown_m)
    {
        PositionUncertaintyNorth_m = positionUncertaintyNorth_m;
        PositionUncertaintyEast_m = positionUncertaintyEast_m;
        PositionUncertaintyDown_m = positionUncertaintyDown_m;
    }
}