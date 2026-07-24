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

        PlatformAutopilot.Update();

        var accelerationTBA = PlatformAutopilot.AccelerationTBA;
        var bankAngleRate_degs = PlatformAutopilot.BankAngleRate_degs;

        var ps = PlatformFunctions.PredictPlatformState(timeStamp, platformState, LLAOrigin.PositionLLA, accelerationTBA, bankAngleRate_degs);

        ps.HeadingAngleDemand_deg = PlatformAutopilot.PlatformFlightpathDemand.HeadingAngleDemand_deg;
        ps.AltitudeDemand_m = PlatformAutopilot.PlatformFlightpathDemand.AltitudeDemand_m;
        ps.TotalSpeedDemand_ms = PlatformAutopilot.PlatformFlightpathDemand.TotalSpeedDemand_ms;
        ps.PitchAngleDemand_deg = PlatformAutopilot.PitchAngleDemand_deg;
        ps.BankAngleDemand_deg = PlatformAutopilot.BankAngleDemand_deg;

        return ps;
    }
}