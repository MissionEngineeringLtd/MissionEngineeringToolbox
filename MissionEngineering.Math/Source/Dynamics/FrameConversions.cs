using static System.Math;

namespace MissionEngineering.Math;

public static class FrameConversions
{
    public static AccelerationNED GetAccelerationNED(AccelerationTBA accelerationTBA, Attitude attitude)
    {
        var t = attitude.GetTransformationMatrix_Inverse();

        var accelerationTBAVector = accelerationTBA.ToVector();

        var accelerationNEDVector = t * accelerationTBAVector;

        var accelerationNED = new AccelerationNED(accelerationNEDVector);

        return accelerationNED;
    }

    public static Attitude GetAttitudeFromVelocityVector(VelocityNED velocityNED)
    {
        var headingAngle_rad = Atan2(velocityNED.VelocityEast_ms, velocityNED.VelocityNorth_ms);
        var pitchAngle_rad = -Asin(velocityNED.VelocityDown_ms / velocityNED.TotalSpeed_ms);

        var headingAngle_deg = headingAngle_rad.RadiansToDegrees();
        var pitchAngle_deg = pitchAngle_rad.RadiansToDegrees();
        var bankAngle_deg = 0.0;

        var attitude = new Attitude(headingAngle_deg, pitchAngle_deg, bankAngle_deg);

        return attitude;
    }

    public static VelocityNED GetVelocityVectorFromAttitude(double totalSpeed_ms, Attitude atittude)
    {
        var headingAngle_rad = atittude.HeadingAngle_deg.DegreesToRadians();
        var pitchAngle_rad = atittude.PitchAngle_deg.DegreesToRadians();

        var velocityNorth_ms = totalSpeed_ms * Cos(headingAngle_rad) * Cos(pitchAngle_rad);
        var velocityEast_ms = totalSpeed_ms * Sin(headingAngle_rad) * Cos(pitchAngle_rad);
        var velocityDown_ms = -totalSpeed_ms * Sin(pitchAngle_rad);

        var velocityNED = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return velocityNED;
    }
}