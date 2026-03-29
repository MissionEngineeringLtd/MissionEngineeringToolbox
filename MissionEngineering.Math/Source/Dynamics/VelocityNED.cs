using CommunityToolkit.Diagnostics;

using static System.Math;

namespace MissionEngineering.Math;

public record VelocityNED
{
    public double VelocityNorth_ms { get; set; }

    public double VelocityEast_ms { get; set; }

    public double VelocityDown_ms { get; set; }

    public double TotalSpeed_ms => GetTotalSpeed_ms();

    public double GroundSpeed_ms => GetGroundSpeed_ms();

    public double VerticalSpeed_ms => -VelocityDown_ms;

    public VelocityNED()
    {
    }

    public VelocityNED(double velocityNorth_ms, double velocityEast_ms, double velocityDown_ms)
    {
        VelocityNorth_ms = velocityNorth_ms;
        VelocityEast_ms = velocityEast_ms;
        VelocityDown_ms = velocityDown_ms;
    }

    public VelocityNED(double[] velocityNED)
    {
        Guard.IsEqualTo(velocityNED.Length, 3, nameof(velocityNED));

        VelocityNorth_ms = velocityNED[0];
        VelocityEast_ms = velocityNED[1];
        VelocityDown_ms = velocityNED[2];
    }

    public VelocityNED(Vector velocityNED)
    {
        Guard.IsEqualTo(velocityNED.NumberOfElements, 3, nameof(velocityNED));

        VelocityNorth_ms = velocityNED[0];
        VelocityEast_ms = velocityNED[1];
        VelocityDown_ms = velocityNED[2];
    }

    public static VelocityNED operator +(VelocityNED left, VelocityNED right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms + right.VelocityNorth_ms;
        var velocityEast_ms = left.VelocityEast_ms + right.VelocityEast_ms;
        var velocityDown_ms = left.VelocityDown_ms + right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator -(VelocityNED left)
    {
        var velocityNorth_ms = -left.VelocityNorth_ms;
        var velocityEast_ms = -left.VelocityEast_ms;
        var velocityDown_ms = -left.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator -(VelocityNED left, VelocityNED right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms - right.VelocityNorth_ms;
        var velocityEast_ms = left.VelocityEast_ms - right.VelocityEast_ms;
        var velocityDown_ms = left.VelocityDown_ms - right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator *(VelocityNED left, double right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms * right;
        var velocityEast_ms = left.VelocityEast_ms * right;
        var velocityDown_ms = left.VelocityDown_ms * right;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator *(double left, VelocityNED right)
    {
        var velocityNorth_ms = left * right.VelocityNorth_ms;
        var velocityEast_ms = left * right.VelocityEast_ms;
        var velocityDown_ms = left * right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static PositionNED operator *(VelocityNED left, DeltaTime right)
    {
        var dt = right.DeltaTime_s;

        var positionNorth_ms = left.VelocityNorth_ms * dt;
        var positionEast_ms = left.VelocityEast_ms * dt;
        var positionDown_ms = left.VelocityDown_ms * dt;

        var result = new PositionNED(positionNorth_ms, positionEast_ms, positionDown_ms);

        return result;
    }

    public static VelocityNED operator /(VelocityNED left, double right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms / right;
        var velocityEast_ms = left.VelocityEast_ms / right;
        var velocityDown_ms = left.VelocityDown_ms / right;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator /(double left, VelocityNED right)
    {
        var velocityNorth_ms = left / right.VelocityNorth_ms;
        var velocityEast_ms = left / right.VelocityEast_ms;
        var velocityDown_ms = left / right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public double GetTotalSpeed_ms()
    {
        var result = Sqrt(VelocityNorth_ms * VelocityNorth_ms + VelocityEast_ms * VelocityEast_ms + VelocityDown_ms * VelocityDown_ms);

        return result;
    }

    public double GetGroundSpeed_ms()
    {
        var result = Sqrt(VelocityNorth_ms * VelocityNorth_ms + VelocityEast_ms * VelocityEast_ms);

        return result;
    }
}