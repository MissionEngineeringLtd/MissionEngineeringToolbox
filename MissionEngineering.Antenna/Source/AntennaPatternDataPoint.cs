namespace MissionEngineering.Antenna;

public record AntennaPatternDataPoint
{
    public double AzimuthAngle_deg { get; set; }

    public double ArrayFactor { get; set; }

    public double ElementFactor { get; set; }

    public double AntennaDirectivity { get; set; }

    public double AntennaGain { get; set; }

    public double ArrayFactor_dB { get; set; }

    public double ElementFactor_dB { get; set; }

    public double AntennaDirectivity_dB { get; set; }

    public double AntennaGain_dB { get; set; }
}
