using MissionEngineering.Core;
using MissionEngineering.Math;
using System.Data;

namespace MissionEngineering.Platform;

public interface IPlatformAutopilot
{
    public PlatformState PlatformState { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    void Initialise();

    void Update();

    void Finalise();
}