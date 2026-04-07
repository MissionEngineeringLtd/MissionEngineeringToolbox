using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public static class PlatformFunctions
{
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

        var (presentationAngleAzimuth_deg, presentationAngleElevation_deg) = CoordinateConversions.CalculatePresentationAngles(relativeVelocityLOS);

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
            PresentationAngleAzimuth_deg = presentationAngleAzimuth_deg,
            PresentationAngleElevation_deg = presentationAngleElevation_deg
        };

        return platformStateRelative;
    }
}