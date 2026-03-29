namespace MissionEngineering.Math;

public record AccelerationNED
{
    public double AccelerationNorth_ms2 { get; init; }

    public double AccelerationEast_ms2 { get; init; }

    public double AccelerationDown_ms2 { get; init; }

    public AccelerationNED()
    {
    }

    public AccelerationNED(double accelerationNorth_ms2, double accelerationEast_ms2, double accelerationDown_ms2)
    {
        AccelerationNorth_ms2 = accelerationNorth_ms2;
        AccelerationEast_ms2 = accelerationEast_ms2;
        AccelerationDown_ms2 = accelerationDown_ms2;
    }

    public AccelerationNED(double[] accelerationNED_ms2)
    {
        AccelerationNorth_ms2 = accelerationNED_ms2[0];
        AccelerationEast_ms2 = accelerationNED_ms2[1];
        AccelerationDown_ms2 = accelerationNED_ms2[2];
    }

    public AccelerationNED(Vector accelerationNED_ms2) : this(accelerationNED_ms2.Data)
    {
    }

    public static AccelerationNED operator +(AccelerationNED left, AccelerationNED right)
    {
        var accelerationNorth_ms2 = left.AccelerationNorth_ms2 + right.AccelerationNorth_ms2;
        var accelerationEast_ms2 = left.AccelerationEast_ms2 + right.AccelerationEast_ms2;
        var accelerationDown_ms2 = left.AccelerationDown_ms2 + right.AccelerationDown_ms2;

        var result = new AccelerationNED(accelerationNorth_ms2, accelerationEast_ms2, accelerationDown_ms2);

        return result;
    }

    //public static AccelerationNED operator -(AccelerationNED left)
    //{
    //    var accelerationNorth = -left.AccelerationNorth;
    //    var accelerationEast = -left.AccelerationEast;
    //    var accelerationDown = -left.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator -(AccelerationNED left, AccelerationNED right)
    //{
    //    var accelerationNorth = left.AccelerationNorth - right.AccelerationNorth;
    //    var accelerationEast = left.AccelerationEast - right.AccelerationEast;
    //    var accelerationDown = left.AccelerationDown - right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator *(AccelerationNED left, double right)
    //{
    //    var accelerationNorth = left.AccelerationNorth * right;
    //    var accelerationEast = left.AccelerationEast * right;
    //    var accelerationDown = left.AccelerationDown * right;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator *(double left, AccelerationNED right)
    //{
    //    var accelerationNorth = left * right.AccelerationNorth;
    //    var accelerationEast = left * right.AccelerationEast;
    //    var accelerationDown = left * right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    public static VelocityNED operator *(AccelerationNED left, DeltaTime right)
    {
        var dt = right.DeltaTime_s;

        var accelerationNorth_ms2 = left.AccelerationNorth_ms2 * dt;
        var accelerationEast_ms2 = left.AccelerationEast_ms2 * dt;
        var accelerationDown_ms2 = left.AccelerationDown_ms2 * dt;

        var result = new VelocityNED(accelerationNorth_ms2, accelerationEast_ms2, accelerationDown_ms2);

        return result;
    }

    //public static AccelerationNED operator /(AccelerationNED left, double right)
    //{
    //    var accelerationNorth = left.AccelerationNorth / right;
    //    var accelerationEast = left.AccelerationEast / right;
    //    var accelerationDown = left.AccelerationDown / right;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator /(double left, AccelerationNED right)
    //{
    //    var accelerationNorth = left / right.AccelerationNorth;
    //    var accelerationEast = left / right.AccelerationEast;
    //    var accelerationDown = left / right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}
}