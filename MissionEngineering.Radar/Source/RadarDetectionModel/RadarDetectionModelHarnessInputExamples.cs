namespace MissionEngineering.Radar;

public static class RadarDetectionModelHarnessInputExamples
{
    public static RadarDetectionModelHarnessInputs Example_1()
    {
        var w = new WaveformParameters()
        {
            WaveformName = "Waveform_1",
            RfFrequency_Hz = 9.376e9,
            PulseWidth_s = 10.07e-6,
            PulseBandwidth_Hz = 5.1e6,
            PulseRepetitionFrequency_Hz = 15000.0,
            NumberOfPulses = 64
        };

        var inputs = new RadarDetectionModelInputs()
        {
            SystemName = "Radar_1",
            SystemProfile = "Profile_1",
            RfSystemType = RfSystemType.MonostaticRadar,
            TransmitPeakPower_W = 2800.0,
            TransmitGain_dB = 34.02,
            ReceiveGain_dB = 32.01,
            ReceiverNoiseFigure_dB = 3.0,
            SystemLosses_dB = 8.0,
            WaveformParameters = w,
            TargetRadarCrossSection_m2 = 5.0
        };

        var harnessInputs = new RadarDetectionModelHarnessInputs()
        {
            RadarDetectionModelInputs = inputs,
            TargetRangeMin_m = 2000,
            TargetRangeMax_m = 200000.0,
            TargetRangeStep_m = 500
        };

        return harnessInputs;
    }
}