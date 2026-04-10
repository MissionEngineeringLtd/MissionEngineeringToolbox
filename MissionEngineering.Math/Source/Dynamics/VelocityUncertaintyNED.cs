namespace MissionEngineering.Math;

public record VelocityUncertaintyNED
{
    public double VelocityUncertaintyNorth_ms { get; init; }

    public double VelocityUncertaintyEast_ms { get; init; }

    public double VelocityUncertaintyDown_ms { get; init; }

    public VelocityUncertaintyNED()
    {
    }

    public VelocityUncertaintyNED(double velocityUncertaintyNorth_ms, double velocityUncertaintyEast_ms, double velocityUncertaintyDown_ms)
    {
        VelocityUncertaintyNorth_ms = velocityUncertaintyNorth_ms;
        VelocityUncertaintyEast_ms = velocityUncertaintyEast_ms;
        VelocityUncertaintyDown_ms = velocityUncertaintyDown_ms;
    }
}