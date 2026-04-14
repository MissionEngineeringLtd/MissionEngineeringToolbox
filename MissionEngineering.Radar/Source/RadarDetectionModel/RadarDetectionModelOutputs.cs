using MissionEngineering.Math;

namespace MissionEngineering.Radar;

public record RadarDetectionModelOutputs
{
    public double SignalPower_W { get; set; }

    public double NoisePower_W { get; set; }

    public double SignalToNoiseRatio => SignalPower_W / NoisePower_W;

    public double SignalPower_dBW => SignalPower_W.PowerToDecibels();

    public double NoisePower_dBW => NoisePower_W.PowerToDecibels();

    public double SignalPower_dBmW => SignalPower_W.PowerToDecibelsm();

    public double NoisePower_dBmW => NoisePower_W.PowerToDecibelsm();

    public double SignalToNoiseRatio_dB => SignalToNoiseRatio.PowerToDecibels();
}