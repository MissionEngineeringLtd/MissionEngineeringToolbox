namespace MissionEngineering.Platform;

public class PlatformDynamics
{
    public double AxialAccelerationGain { get; set; }

    public double AxialAccelerationMax_ms2 { get; set; }

    public double LateralAccelerationGain { get; set; }

    public double LateralAccelerationMax_ms2 { get; set; }

    public double VerticalAccelerationGain { get; set; }

    public double VerticalAccelerationMax_ms2 { get; set; }

    public double PitchAngleGain { get; set; }

    public double PitchAngleMax_deg { get; set; }

    public double BankAngleRateGain { get; set; }

    public double BankAngleRateMax_degs { get; set; }

    public PlatformDynamics()
    {
        AxialAccelerationGain = 20.0;
        AxialAccelerationMax_ms2 = 20.0;

        LateralAccelerationGain = 10.0;
        LateralAccelerationMax_ms2 = 30.0;

        VerticalAccelerationGain = -10.0;
        VerticalAccelerationMax_ms2 = 20.0;

        PitchAngleGain = 0.05;
        PitchAngleMax_deg = 20.0;

        BankAngleRateGain = 5;
        BankAngleRateMax_degs = 60.0;
    }
}