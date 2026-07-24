using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public interface IPlatformAutopilot
{
    PlatformState PlatformState { get; set; }

    PlatformFlightpathDemand PlatformFlightpathDemand { get; set; }

    AccelerationTBA AccelerationTBA { get; set; }

    double PitchAngleDemand_deg { get; set; }

    double BankAngleDemand_deg { get; set; }

    double BankAngleRate_degs { get; set; }

    void Initialise();

    void Update();

    void Finalise();
}