namespace MissionEngineering.MathLibrary;

public record AttitudeRate
{
    public double HeadingAngleRate_degs { get; init; }

    public double PitchAngleRate_degs { get; init; }

    public double BankAngleRate_degs { get; init; }

    public AttitudeRate()
    {
    }

    public AttitudeRate(double headingAngleRate_degs, double pitchAngleRate_degs, double bankAngleRate_degs)
    {
        HeadingAngleRate_degs = headingAngleRate_degs;
        PitchAngleRate_degs = pitchAngleRate_degs;
        BankAngleRate_degs = bankAngleRate_degs;
    }
}