using MissionEngineering.Core;
using MissionEngineering.Math;
using System.Data;

namespace MissionEngineering.Platform;

public interface IPlatformAutopilot
{
    PlatformState PlatformState { get; set; }

    PlatformFlightpathDemand PlatformFlightpathDemand { get; set; }

    AccelerationTBA AccelerationTBA { get; set; }

    double PitchAngleDemand_deg { get; set; }

    void Initialise();

    void Update();

    void Finalise();
}