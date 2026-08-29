using MissionEngineering.Math;

namespace MissionEngineering.Radar;

public record RadarDetectionModelInputs
{
    public string SystemName { get; set; }

    public string SystemProfile { get; set; }

    public RfSystemType RfSystemType { get; set; }

    public double TransmitPeakPower_W { get; set; }

    public double TransmitPeakPower_mW => TransmitPeakPower_W * 1000.0;

    public double TransmitPeakPower_dBW => TransmitPeakPower_W.PowerToDecibels();

    public double TransmitPeakPower_dBmW => TransmitPeakPower_mW.PowerToDecibels();

    public double TransmitGain { get; set; }

    public double TransmitGain_dB
    {
        get => TransmitGain.PowerToDecibels();
        set => TransmitGain = value.DecibelsToPower();
    }

    public double EIRP_W => TransmitPeakPower_W * TransmitGain;

    public double EIRP_mW => EIRP_W * 1000.0;

    public double EIRP_dBW => EIRP_W.PowerToDecibels();

    public double EIRP_dBmW => EIRP_mW.PowerToDecibels();

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

    public double TargetRange_NM => TargetRange_m.MetersToNauticalMiles();

    public double TargetRangeRate_ms { get; set; }

    public double TargetRadarCrossSection_m2 { get; set; }

    public double TargetRadarCrossSection_dBsm
    {
        get => TargetRadarCrossSection_m2.PowerToDecibels();
        set => TargetRadarCrossSection_m2 = value.DecibelsToPower();
    }
}