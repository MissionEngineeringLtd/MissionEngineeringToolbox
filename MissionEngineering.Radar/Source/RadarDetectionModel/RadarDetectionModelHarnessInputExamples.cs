namespace MissionEngineering.Radar;

public static class RadarDetectionModelHarnessInputExamples
{
    public static RadarDetectionModelHarnessInputs Example_1()
    {
        var w = new WaveformParameters()
        {
            WaveformName = "Waveform_1",
            RfFrequency_Hz = 9.0e9,
            PulseWidth_s = 1.0e-6,
            PulseBandwidth_Hz = 5.0e6,
            PulseRepetitionFrequency_Hz = 150000.0,
            NumberOfPulses = 1024
        };

        var inputs = new RadarDetectionModelInputs()
        {
            RadarName = "Radar_1",
            RadarProfile = "Profile_1",
            RfSystemType = RfSystemType.MonostaticRadar,
            TransmitPeakPower_W = 8000.0,
            TransmitGain_dB = 37.0,
            ReceiveGain_dB = 35.0,
            ReceiverNoiseFigure_dB = 3.0,
            SystemLosses_dB = 5.0,
            WaveformParameters = w,
            TargetRadarCrossSection_m2 = 10.0
        };

        var harnessInputs = new RadarDetectionModelHarnessInputs()
        {
            RadarDetectionModelInputs = inputs,
            TargetRangeMin_m = 100,
            TargetRangeMax_m = 200000.0,
            TargetRangeStep_m = 100
        };

        return harnessInputs;
    }
}