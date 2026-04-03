using MissionEngineering.Math;

namespace MissionEngineering.Radar;

public class TransmitterParameters
{
    public double TransmitPower { get; set; }

    public double TransmitPower_dB
    {
        get => TransmitPower.PowerToDecibels();
        set => TransmitPower = value.DecibelsToPower();
    }

    public double TransmitPower_dBm
    {
        get => TransmitPower.PowerToDecibelsm();
        set => TransmitPower = value.DecibelsmToPower();
    }
}