using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Antenna;

public static class AntennaModelSettingsExamples
{
    public static AntennaModelSettings Example_1()
    {
        var settings = new AntennaModelSettings
        {
            AntennaName = "Example_1",
            RfFrequency_Hz = 10.0e9,
            AntennaWidth_m = 1.0,
            AntennaElementSpacing_wavelengths = 1.3,
            AntennaLosses_dB = 3.0,
            AzimuthAngleMin_deg = -60.0,
            AzimuthAngleMax_deg = 80.0,
            AzimuthAngleStep_deg = 0.01
        };

        return settings;
    }
}
