using MissionEngineering.Math;

namespace MissionEngineering.Antenna;

public class AntennaPattern
{
    public string AntennaPatternName { get; set; }

    public string AntennaPatternFileName { get; set; }

    public Vector AzimuthAngles_deg { get; set; }

    public int NumberOfAzimuthAngles => AzimuthAngles_deg.NumberOfElements;

    public Vector AntennaDirectivity { get; set; }

    public Vector AntennaGain { get; set; }

    public Vector AntennaDirectivity_dB { get; set; }

    public Vector AntennaGain_dB { get; set; }

    public Vector AntennaDirectivityNormalised_dB { get; set; }

    public Vector AntennaGainNormalised_dB { get; set; }
}
