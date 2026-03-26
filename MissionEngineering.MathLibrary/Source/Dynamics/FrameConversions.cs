using static System.Math;

namespace MissionEngineering.MathLibrary;

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
}