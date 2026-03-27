using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class PlatformModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformModel(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public PlatformState Update(double time_s, PlatformState platformState)
    {
        var deltaTime = time_s - platformState.Time_s;

        var dt = new DeltaTime(deltaTime);

        var accelerationTBA = GetAccelerationTBA();

        var accelerationNED = GetAccelerationNED();

        var velocityNED = platformState.VelocityNED + accelerationNED * dt;
        var positionNED = platformState.PositionNED + velocityNED * dt;

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        var attitude = GetAttitude();

        var attitudeRate = GetAttitudeRate();

        var ps = new PlatformState
        {
            Time_s = time_s,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude,
            AttitudeRate = attitudeRate
        };

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

    public AccelerationNED GetAccelerationNED()
    {
        var accelerationNED = new AccelerationNED
        {
            AccelerationNorth_ms2 = 0.0,
            AccelerationEast_ms2 = 0.0,
            AccelerationDown_ms2 = 0.0
        };

        return accelerationNED;
    }

    public Attitude GetAttitude()
    {
        var attitude = new Attitude
        {
             HeadingAngle_deg = 0.0,
             BankAngle_deg = 0.0,
             PitchAngle_deg = 0.0,
        };

        return attitude;
    }

    public AttitudeRate GetAttitudeRate()
    {
        var attitudeRate = new AttitudeRate
        {
            HeadingAngleRate_degs = 0.0,
            BankAngleRate_degs = 0.0,
            PitchAngleRate_degs = 0.0,
        };

        return attitudeRate;
    }
}