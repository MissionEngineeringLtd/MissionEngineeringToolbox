using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public static class PlatformFunctions
{
    public static PlatformState PredictPlatformState(SimulationTimeStamp timeStamp, PlatformState platformState, PositionLLA positionLLAOrigin, AccelerationTBA accelerationTBA, bool isPredict)
    {
        var deltaTime_s = timeStamp.SimulationTime_s - platformState.TimeStamp.SimulationTime_s;

        var dt = new DeltaTime(deltaTime_s);

        var accelerationNED = FrameConversions.GetAccelerationNED(accelerationTBA, platformState.Attitude);

        var velocityNED = platformState.VelocityNED + accelerationNED * dt;
        var positionNED = platformState.PositionNED + velocityNED * dt;

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, positionLLAOrigin);

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(platformState.VelocityNED);
        var attitudeRate = GetAttitudeRate(platformState.Attitude, attitude, dt);

        var lastUpdateTime_s = platformState.TimeStamp.SimulationTime_s;
        var predictionTime_s = timeStamp.SimulationTime_s;
        var predictionTimeDelta_s = predictionTime_s - lastUpdateTime_s;

        var ps = platformState with
        {
            TimeStamp = timeStamp,
            IsPrediction = isPredict,
            LastUpdateTime_s = platformState.TimeStamp.SimulationTime_s,
            PredictionTime_s = predictionTime_s,
            PredictionTimeDelta_s = predictionTimeDelta_s,
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

    public static AttitudeRate GetAttitudeRate(Attitude attitudePrevious, Attitude attitudeCurrent, DeltaTime deltaTime_s)
    {
        if (deltaTime_s.DeltaTime_s == 0.0)
        {
            return new AttitudeRate
            {
                HeadingRate_degs = 0.0,
                PitchRate_degs = 0.0,
                BankRate_degs = 0.0
            };
        }

        var attitudeRate = (attitudeCurrent - attitudePrevious) / deltaTime_s;

        return attitudeRate;
    }

    public static PlatformStateRelative GeneratePlatformStateRelative(PlatformState platformStateOrigin, PlatformState platformStateTarget)
    {
        var relativePositionNED = platformStateTarget.PositionNED - platformStateOrigin.PositionNED;

        var relativeVelocityNED = platformStateTarget.VelocityNED - platformStateOrigin.VelocityNED;

        var relativePolarsNED = CoordinateConversions.CartesiansToPolars(relativePositionNED, relativeVelocityNED);

        var lineOfSightAzimuthAngle_deg = relativePolarsNED.AzimuthAngle_rad.RadiansToDegrees();
        var lineOfSightElevationAngle_deg = relativePolarsNED.ElevationAngle_rad.RadiansToDegrees();

        var lineOfSightAttitude = new Attitude(lineOfSightAzimuthAngle_deg, lineOfSightElevationAngle_deg, 0.0);

        var rotationMatrix = lineOfSightAttitude.GetTransformationMatrix();

        var relativePositionLOS = relativePositionNED.Rotate(rotationMatrix);

        var relativeVelocityLOS = relativeVelocityNED.Rotate(rotationMatrix);

        var relativePolarsLOS = CoordinateConversions.CartesiansToPolars(relativePositionLOS, relativeVelocityLOS);

        var (aspectAngleAzimuth_deg, aspectAngleElevation_deg) = CoordinateConversions.CalculateAspectAngles(relativeVelocityLOS);

        var platformStateRelative = new PlatformStateRelative
        {
            TimeStamp = platformStateOrigin.TimeStamp,
            PlatformIdOrigin = platformStateOrigin.PlatformId,
            PlatformNameOrigin = platformStateOrigin.PlatformName,
            PlatformIdTarget = platformStateTarget.PlatformId,
            PlatformNameTarget = platformStateTarget.PlatformName,
            RelativePositionNED = relativePositionNED,
            RelativeVelocityNED = relativeVelocityNED,
            RelativePositionLOS = relativePositionLOS,
            RelativeVelocityLOS = relativeVelocityLOS,
            RelativePolarsNED = relativePolarsNED,
            RelativePolarsLOS = relativePolarsLOS,
            AspectAngleAzimuth_deg = aspectAngleAzimuth_deg,
            AspectAngleElevation_deg = aspectAngleElevation_deg
        };

        return platformStateRelative;
    }
}