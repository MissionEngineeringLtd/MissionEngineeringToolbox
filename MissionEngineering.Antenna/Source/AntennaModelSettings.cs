using MissionEngineering.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Antenna;

public class AntennaModelSettings
{
    public string AntennaName { get; set; } = "Undefined";

    public double RfFrequency_Hz { get; set; }

    public double RfFrequency_kHz
    {
        get => RfFrequency_Hz / 1.0e3;
        set => RfFrequency_Hz = value * 1.0e3;
    }

    public double RfFrequency_MHz
    {
        get => RfFrequency_Hz / 1.0e6;
        set => RfFrequency_Hz = value * 1.0e6;
    }

    public double RfFrequency_GHz
    {
        get => RfFrequency_Hz / 1.0e9;
        set => RfFrequency_Hz = value * 1.0e9;
    }

    public double RfWavelength_m
    {
        get => RfFrequency_Hz.FrequencyToWavelength();
        set => RfFrequency_Hz = value.WavelengthToFrequency();
    }

    public double RfWavelength_cm
    {
        get => RfWavelength_m * 100.0;
        set => RfWavelength_m = value / 100.0;
    }

    public double AntennaWidth_m { get; set; }

    public double AntennaElementSpacing_m { get; set; }

    public double AntennaElementSpacing_wavelengths
    {
        get => AntennaElementSpacing_m / RfWavelength_m;
        set => AntennaElementSpacing_m = value * RfWavelength_m;
    }

    public int NumberOfAntennaElements => (int)(AntennaWidth_m / AntennaElementSpacing_m);

    public double AntennaLosses { get; set; }

    public double AntennaLosses_dB
    {
        get => AntennaLosses.PowerToDecibels();
        set => AntennaLosses = value.DecibelsToPower();
    }

    public double AzimuthAngleMin_deg { get; set; }

    public double AzimuthAngleMax_deg { get; set; }

    public double AzimuthAngleStep_deg { get; set; }
}
