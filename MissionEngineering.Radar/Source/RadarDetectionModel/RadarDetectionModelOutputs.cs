using MissionEngineering.Math;

namespace MissionEngineering.Radar;

public record RadarDetectionModelOutputs
{
    public double SignalPower_W { get; set; }

    public double NoisePower_W { get; set; }

    public double SignalToNoiseRatio => SignalPower_W / NoisePower_W;

    public double SignalPower_dB => SignalPower_W.PowerToDecibels();

    public double NoisePower_dB => NoisePower_W.PowerToDecibels();

    public double SignalPower_dBm => SignalPower_W.PowerToDecibelsm();

    public double NoisePower_dBm => NoisePower_W.PowerToDecibelsm();

    public double SignalToNoiseRatio_dB => SignalToNoiseRatio.PowerToDecibels();
}