namespace MissionEngineering.Math;

public record AttitudeRate
{
    public double HeadingRate_degs { get; init; }

    public double PitchRate_degs { get; init; }

    public double BankRate_degs { get; init; }

    public AttitudeRate()
    {
    }

    public AttitudeRate(double headingRate_degs, double pitchRate_degs, double bankRate_degs)
    {
        HeadingRate_degs = headingRate_degs;
        PitchRate_degs = pitchRate_degs;
        BankRate_degs = bankRate_degs;
    }
}