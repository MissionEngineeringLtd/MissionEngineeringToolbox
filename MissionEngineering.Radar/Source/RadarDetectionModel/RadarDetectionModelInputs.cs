using MissionEngineering.Math;

namespace MissionEngineering.Radar;

public record RadarDetectionModelInputs
{
    public string RadarName { get; set; }

    public string RadarProfile { get; set; }

    public RfSystemType RfSystemType { get; set; }

    public double TransmitPeakPower_W { get; set; }

    public double TransmitPeakPower_dB => TransmitPeakPower_W.PowerToDecibels();

    public double TransmitGain { get; set; }

    public double TransmitGain_dB
    {
        get => TransmitGain.PowerToDecibels();
        set => TransmitGain = value.DecibelsToPower();
    }

    public double EIRP_W => TransmitPeakPower_W * TransmitGain;

    public double EIRP_dB => EIRP_W.PowerToDecibels();

    public double ReceiveGain { get; set; }

    public double ReceiveGain_dB
    {
        get => ReceiveGain.PowerToDecibels();
        set => ReceiveGain = value.DecibelsToPower();
    }

    public double ReceiverNoiseFigure { get; set; }

    public double ReceiverNoiseFigure_dB
    {
        get => ReceiverNoiseFigure.PowerToDecibels();
        set => ReceiverNoiseFigure = value.DecibelsToPower();
    }

    public double SystemLosses { get; set; }

    public double SystemLosses_dB
    {
        get => SystemLosses.PowerToDecibels();
        set => SystemLosses = value.DecibelsToPower();
    }

    public double AtmosphericLoss_dB_per_km { get; set; }

    public WaveformParameters WaveformParameters { get; set; }

    public double TargetRange_m { get; set; }

    public double TargetRange_km => TargetRange_m / 1000.0;

    public double TargetRangeRate_ms { get; set; }

    public double TargetRadarCrossSection_m2 { get; set; }

    public double TargetRadarCrossSection_dBsm
    {
        get => TargetRadarCrossSection_m2.PowerToDecibels();
        set => TargetRadarCrossSection_m2 = value.DecibelsToPower();
    }
}