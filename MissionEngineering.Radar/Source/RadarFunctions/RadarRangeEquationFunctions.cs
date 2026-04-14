using MissionEngineering.Math;

using static System.Math;

namespace MissionEngineering.Radar;

public static class RadarRangeEquationFunctions
{
    public static double CalculateSignalPower_W(RadarDetectionModelInputs inputs)
    {
        var i = inputs;
        var w = inputs.WaveformParameters;

        var signalPower_W = CalculateSignalPower_W(i.TransmitPeakPower_W, w.RfWavelength_m, i.TransmitGain_dB, i.ReceiveGain_dB, w.PulseCompressionRatio, w.NumberOfPulses, i.SystemLosses_dB, i.TargetRange_m, i.TargetRangeRate_ms, i.TargetRadarCrossSection_m2, i.AtmosphericLoss_dB_per_km);

        return signalPower_W;
    }

    public static double CalculateNoisePower_W(RadarDetectionModelInputs inputs)
    {
        var i = inputs;
        var w = inputs.WaveformParameters;

        var noisePower_W = CalculateNoisePower_W(w.PulseBandwidth_Hz, i.ReceiverNoiseFigure_dB);

        return noisePower_W;
    }

    public static double CalculateAtmosphericLoss_dB(RadarDetectionModelInputs inputs, double targetRange_m)
    {
        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(inputs.AtmosphericLoss_dB_per_km, targetRange_m);

        return atmosphericLoss_dB;
    }

    public static double CalculateSignalPower_W(double transmitPower_W, double rfCenterWavelength_m, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseCompressionRatio, int numberOfPulses, double systemLosses_dB, double targetRange_m, double targetRangeRate_ms, double radarCrossSection_m2, double atmophericLoss_dB_per_km)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();

        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(atmophericLoss_dB_per_km, targetRange_m, isTwoWay: true);
        var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();

        var systemLosses = systemLosses_dB.DecibelsToPower();

        var numerator = transmitPower_W * antennaGainTransmit * antennaGainReceive * rfCenterWavelength_m * rfCenterWavelength_m * pulseCompressionRatio * radarCrossSection_m2 * numberOfPulses;
        var denominator = Pow(4 * PI, 3) * Pow(targetRange_m, 4) * systemLosses * atmosphericLoss;

        var signalPower_W = numerator / denominator;

        return signalPower_W;
    }

    public static double CalculateNoisePower_W(double noiseBandwidth_Hz, double noiseFigure_dB)
    {
        var noiseFigure = noiseFigure_dB.DecibelsToPower();

        var noisePower_W = PhysicalConstants.BoltzmannConstant * PhysicalConstants.SystemReferenceTemperature * noiseBandwidth_Hz * noiseFigure;

        return noisePower_W;
    }

    public static double CalculateAtmosphericLoss_dB(double atmophericLoss_dB_per_km, double targetRange_m, bool isTwoWay = true)
    {
        var atmosphericLoss_dB = atmophericLoss_dB_per_km * targetRange_m / 1000.0;

        if (isTwoWay)
        {
            atmosphericLoss_dB *= 2.0;
        }

        return atmosphericLoss_dB;
    }
}