using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public class PlatformModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformModel(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public PlatformState Update(SimulationTimeStamp timeStamp, PlatformState platformState)
    {
        // For update, we  use the actual acceleration in the TBA frame.
        var accelerationTBA = GetAccelerationTBA();

        var ps = PlatformFunctions.PredictPlatformState(timeStamp, platformState, LLAOrigin.PositionLLA, accelerationTBA, false);

        return ps;
    }

    public PlatformState Predict(SimulationTimeStamp timeStamp, PlatformState platformState)
    {
        // For prediction, we assume zero acceleration in the TBA frame for simplicity.
        var accelerationTBA = new AccelerationTBA
        {
            AccelerationAxial_ms2 = 0.0,
            AccelerationLateral_ms2 = 0.0,
            AccelerationVertical_ms2 = 0.0
        };

        var ps = PlatformFunctions.PredictPlatformState(timeStamp, platformState, LLAOrigin.PositionLLA, accelerationTBA, false);

        return ps;
    }

    public AccelerationTBA GetAccelerationTBA()
    {
        var accelerationTBA = new AccelerationTBA
        {
            AccelerationAxial_ms2 = 0.0,
            AccelerationLateral_ms2 = 0.0,
            AccelerationVertical_ms2 = 0.0
        };

        return accelerationTBA;
    }
}