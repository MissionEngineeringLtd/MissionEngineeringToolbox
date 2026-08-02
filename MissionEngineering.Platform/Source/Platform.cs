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

    public PlatformData PlatformData { get; set; }

    public List<PlatformData> PlatformDataList { get; set; }

    public Platform PlatformTarget { get; set; }

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

        var platformAutopilot = new PlatformAutopilot();

        platformAutopilot.Initialise();

        PlatformModel.PlatformAutopilot = platformAutopilot;

        PlatformModel.PlatformAutopilot.PlatformTarget = PlatformTarget;

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

        platformAutopilot.PlatformFlightpathDemand = new PlatformFlightpathDemand()
        {
            HeadingAngleDemand_deg = attitude.HeadingAngle_deg + 20,
            AltitudeDemand_m = positionLLA.Altitude_m,
            TotalSpeedDemand_ms = velocityNED.TotalSpeed_ms
        };

        PlatformState = new PlatformState
        {
            TimeStamp = timeStamp,
            PlatformId = PlatformSettings.PlatformHeader.PlatformId,
            PlatformName = PlatformSettings.PlatformHeader.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude,
            HeadingAngleDemand_deg = platformAutopilot.PlatformFlightpathDemand.HeadingAngleDemand_deg,
            AltitudeDemand_m = platformAutopilot.PlatformFlightpathDemand.AltitudeDemand_m,
            TotalSpeedDemand_ms = platformAutopilot.PlatformFlightpathDemand.TotalSpeedDemand_ms,
            PitchAngleDemand_deg = platformAutopilot.PitchAngleDemand_deg,
        };
    }

    public void Update(double time_s)
    {
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        PlatformState = PlatformModel.Update(timeStamp, PlatformState);

        (PlatformState.RangeToGo_m, PlatformState.TimeToGo_s) = GenerateRangeToGo(PlatformState.PositionNED, PlatformState.VelocityNED);

        CheckIfDestroyed();

        PlatformData = new PlatformData
        {
            PlatformHeader = PlatformSettings.PlatformHeader,
            PlatformHeaderSimdis = PlatformSettings.PlatformHeaderSimdis,
            PlatformState = PlatformState
        };

        PlatformDataList.Add(PlatformData);
    }

    public void Finalise(double time_s)
    {
    }

    public (double, double) GenerateRangeToGo(PositionNED positionNED, VelocityNED velocityNED)
    {
        if (PlatformTarget is null)
        {
            return (0.0, 0.0);
        }

        if (PlatformState.IsDestroyed)
        {
            return (0.0, 0.0);
        }

        var positionNEDTarget = PlatformTarget.PlatformState.PositionNED;
        var velocityNEDTarget = PlatformTarget.PlatformState.VelocityNED;

        var relativePositionNED = positionNEDTarget - positionNED;
        var relativeVelocityNED = velocityNEDTarget - velocityNED;

        var relativePolarsNED = CoordinateConversions.CartesiansToPolars(relativePositionNED, relativeVelocityNED);

        var rangeToGo = relativePolarsNED.Range_m;
        var rangeRate = relativePolarsNED.RangeRate_ms;

        var timeToGo = -rangeToGo / rangeRate;

        return (rangeToGo, timeToGo);
    }

    public void CheckIfDestroyed()
    {
        if (PlatformTarget is null) { return; }

        if (!PlatformState.IsDestroyed && PlatformState.RangeToGo_m < 50.0)
        {
            PlatformState.IsDestroyed = true;
        }
    }
}