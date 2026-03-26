namespace MissionEngineering.MathLibrary;

public record AccelerationTBA
{
    public double AccelerationAxial_ms2 { get; init; }

    public double AccelerationLateral_ms2 { get; init; }

    public double AccelerationVertical_ms2 { get; init; }

    public AccelerationTBA()
    {
    }

    public AccelerationTBA(double accelerationAxial_ms2, double accelerationLateral_ms2, double accelerationVertical_ms2)
    {
        AccelerationAxial_ms2 = accelerationAxial_ms2;
        AccelerationLateral_ms2 = accelerationLateral_ms2;
        AccelerationVertical_ms2 = accelerationVertical_ms2;
    }

    public Vector ToVector()
    {
        var result = new Vector(AccelerationAxial_ms2, AccelerationLateral_ms2, AccelerationVertical_ms2);

        return result;
    }
}