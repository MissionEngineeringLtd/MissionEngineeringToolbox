using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public class Platform : IExecutableModel
{
    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformSettings PlatformSettings { get; set; }

    public PlatformModel PlatformModel { get; set; }

    public PlatformState PlatformState { get; set; }

    public List<PlatformData> PlatformDataList { get; set; }

    public Platform(ISimulationClock simulationClock, ILLAOrigin llaOrigin)
    {
        SimulationClock = simulationClock;

        LLAOrigin = llaOrigin;

        PlatformDataList = [];
    }

    public void Initialise(double time_s)
    {
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var pi = PlatformSettings.PlatformStateInitial;

        PlatformModel = new PlatformModel(LLAOrigin);

        var attitude = new Attitude
        {
            HeadingAngle_deg = pi.HeadingAngle_deg,
            PitchAngle_deg = pi.PitchAngle_deg,
            BankAngle_deg = 0.0
        };

        var positionNED = new PositionNED
        {
            PositionNorth_m = pi.PositionNorth_m,
            PositionEast_m = pi.PositionEast_m,
            PositionDown_m = -pi.Altitude_m
        };

        var velocityNED = FrameConversions.GetVelocityVectorFromAttitude(pi.TotalSpeed_ms, attitude);

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        PlatformState = new PlatformState
        {
            TimeStamp = timeStamp,
            IsPrediction = false,
            PredictionTime_s = 0.0,
            PlatformId = PlatformSettings.PlatformHeader.PlatformId,
            PlatformName = PlatformSettings.PlatformHeader.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude,
        };
    }

    public void Update(double time_s)
    { 
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        PlatformState = PlatformModel.Update(timeStamp, PlatformState);

        var platformData = new PlatformData
        {
            PlatformHeader = PlatformSettings.PlatformHeader,
            PlatformHeaderSimdis = PlatformSettings.PlatformHeaderSimdis,
            PlatformState = PlatformState
        };

        PlatformDataList.Add(platformData);
    }

    public PlatformState Predict(double time_s)
    {
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var ps = PlatformModel.Predict(timeStamp, PlatformState);

        return ps;
    }

    public void Finalise(double time_s)
    {
    }
}