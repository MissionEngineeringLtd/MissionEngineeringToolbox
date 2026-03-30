using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public class PlatformModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformModel(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public PlatformState Update(DateTime dateTime, double time_s, PlatformState platformState)
    {
        var deltaTime = time_s - platformState.Time_s;

        var dt = new DeltaTime(deltaTime);

        var accelerationTBA = GetAccelerationTBA();

        var accelerationNED = GetAccelerationNED(accelerationTBA, platformState.Attitude);

        var velocityNED = platformState.VelocityNED + accelerationNED * dt;
        var positionNED = platformState.PositionNED + velocityNED * dt;

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(platformState.VelocityNED);
        var attitudeRate = GetAttitudeRate(platformState.Attitude, attitude, dt);

        var ps = platformState with
        {
            DateTime = dateTime,
            Time_s = time_s,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            AccelerationNED = accelerationNED,
            AccelerationTBA = accelerationTBA,
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

    public AccelerationNED GetAccelerationNED(AccelerationTBA accelerationTBA, Attitude attitude)
    {
        var accelerationNED = FrameConversions.GetAccelerationNED(accelerationTBA, attitude);

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

    public AttitudeRate GetAttitudeRate(Attitude attitudePrevious, Attitude attitudeCurrent, DeltaTime deltaTime_s)
    {
        if (deltaTime_s.DeltaTime_s == 0.0)
        {
            return new AttitudeRate
            {
                HeadingAngleRate_degs = 0.0,
                PitchAngleRate_degs = 0.0,
                BankAngleRate_degs = 0.0
            };
        }

        var attitudeRate = (attitudeCurrent - attitudePrevious) / deltaTime_s;

        return attitudeRate;
    }
}