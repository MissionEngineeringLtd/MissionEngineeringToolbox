using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public class PlatformModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public IPlatformAutopilot PlatformAutopilot { get; set; }

    public PlatformModel(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public PlatformState Update(SimulationTimeStamp timeStamp, PlatformState platformState)
    {
        PlatformAutopilot.PlatformState = platformState;

        var accelerationTBA = GetAccelerationTBA();

        var ps = PlatformFunctions.PredictPlatformState(timeStamp, platformState, LLAOrigin.PositionLLA, accelerationTBA);

        ps.HeadingAngleDemand_deg = PlatformAutopilot.PlatformFlightpathDemand.HeadingAngleDemand_deg;
        ps.AltitudeDemand_m = PlatformAutopilot.PlatformFlightpathDemand.AltitudeDemand_m;
        ps.TotalSpeedDemand_ms = PlatformAutopilot.PlatformFlightpathDemand.TotalSpeedDemand_ms;
        ps.PitchAngleDemand_deg = PlatformAutopilot.PitchAngleDemand_deg;

        return ps;
    }

    public AccelerationTBA GetAccelerationTBA()
    {
        PlatformAutopilot.Update();

        var accelerationTBA = PlatformAutopilot.AccelerationTBA;

        return accelerationTBA;
    }
}