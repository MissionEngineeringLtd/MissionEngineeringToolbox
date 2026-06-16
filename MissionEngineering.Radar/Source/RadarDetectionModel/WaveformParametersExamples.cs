using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Radar;

public static class WaveformParametersExamples
{
    public static WaveformParameters Waveform_1()
    {
        var waveformParameters = new WaveformParameters()
        {
            WaveformName = "Waveform_1",
            RfFrequency_Hz = 9.0e9,
            PulseWidth_s = 1.0e-6,
            PulseBandwidth_Hz = 5.0e6,
            PulseRepetitionFrequency_Hz = 150000.0,
            NumberOfPulses = 1024
        };
        return waveformParameters;
    }
}
