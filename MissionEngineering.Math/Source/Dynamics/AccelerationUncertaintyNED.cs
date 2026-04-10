namespace MissionEngineering.Math;

public record AccelerationUncertaintyNED
{
    public double AccelerationUncertaintyNorth_ms2 { get; init; }

    public double AccelerationUncertaintyEast_ms2 { get; init; }

    public double AccelerationUncertaintyDown_ms2 { get; init; }

    public AccelerationUncertaintyNED()
    {
    }

    public AccelerationUncertaintyNED(double accelerationUncertaintyNorth_ms2, double accelerationUncertaintyEast_ms2, double accelerationUncertaintyDown_ms2)
    {
        AccelerationUncertaintyNorth_ms2 = accelerationUncertaintyNorth_ms2;
        AccelerationUncertaintyEast_ms2 = accelerationUncertaintyEast_ms2;
        AccelerationUncertaintyDown_ms2 = accelerationUncertaintyDown_ms2;
    }
}