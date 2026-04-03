using static MissionEngineering.Math.PhysicalConstants;
using static MissionEngineering.Radar.RadarFunctions;

namespace MissionEngineering.Radar;

public record WaveformParameters
{
    public string WaveformName { get; set; } = "Undefined";

    public double RfFrequency_Hz { get; set; }

    public double RfFrequency_kHz
    {
        get => RfFrequency_Hz / 1.0e3;
        set => RfFrequency_Hz = value * 1.0e3;
    }

    public double RfFrequency_MHz
    {
        get => RfFrequency_Hz / 1.0e6;
        set => RfFrequency_Hz = value * 1.0e6;
    }

    public double RfFrequency_GHz
    {
        get => RfFrequency_Hz / 1.0e9;
        set => RfFrequency_Hz = value * 1.0e9;
    }

    public double RfWavelength_m
    {
        get => SpeedOfLight / RfFrequency_Hz;
        set => RfFrequency_Hz = SpeedOfLight / value;
    }

    public double RfWavelength_cm
    {
        get => RfWavelength_m * 100.0;
        set => RfWavelength_m = value / 100.0;
    }

    public double RfWavelength_mm
    {
        get => RfWavelength_m * 1000.0;
        set => RfWavelength_m = value / 1000.0;
    }

    public double PulseWidth_s { get; set; }

    public double PulseWidth_us
    {
        get => PulseWidth_s * 1.0e6;
        set => PulseWidth_s = value * 1.0e-6;
    }

    public double PulseBandwidth_Hz { get; set; }

    public double PulseRepetitionFrequency_Hz { get; set; }

    public double PulseRepetitionFrequency_kHz => PulseRepetitionFrequency_Hz / 1000.0;

    public double PulseRepetitionInterval_s
    {
        get => 1.0 / PulseRepetitionFrequency_Hz;
        set => PulseRepetitionFrequency_Hz = 1.0 / value;
    }

    public double PulseRepetitionInterval_ms => PulseRepetitionInterval_s * 1000.0;

    public int NumberOfPulses { get; set; }

    public double BurstTime_s => PulseRepetitionInterval_s * NumberOfPulses;

    public double BurstTime_ms => BurstTime_s * 1000.0;

    public double DutyRatio => PulseWidth_s * PulseRepetitionFrequency_Hz;

    public double DutyRatioPercent => DutyRatio * 100.0;

    public double UncompressedPulseWidth_m => CalculateTwoWayRangeFromDelayTime(PulseWidth_s);

    public double CompressedPulseWidth_m => UncompressedPulseWidth_m / PulseCompressionRatio;

    public double PulseCompressionRatio => PulseWidth_s * PulseBandwidth_Hz;

    public double MaximumUnambiguousRange_m => CalculateMaximumUnambiguousRange(PulseRepetitionFrequency_Hz);

    public double MaximumUnambiguousRangeRate_ms => CalculateMaximumUnambiguousRangeRate(RfFrequency_Hz, PulseRepetitionFrequency_Hz);

    public double RangeResolution_m => CompressedPulseWidth_m;

    public double VelocityResolution_ms => MaximumUnambiguousRangeRate_ms / NumberOfPulses;
}