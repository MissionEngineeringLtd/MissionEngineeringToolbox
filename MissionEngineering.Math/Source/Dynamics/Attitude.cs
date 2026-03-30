using static System.Math;

namespace MissionEngineering.Math;

public record Attitude
{
    public double HeadingAngle_deg { get; set; }

    public double PitchAngle_deg { get; set; }

    public double BankAngle_deg { get; set; }

    public Attitude()
    {
    }

    public Attitude(double headingAngle_deg, double pitchAngle_deg, double bankAngle_deg)
    {
        HeadingAngle_deg = headingAngle_deg;
        PitchAngle_deg = pitchAngle_deg;
        BankAngle_deg = bankAngle_deg;
    }

    public static Attitude operator +(Attitude x, Attitude y)
    {
        var headingAngle_deg = x.HeadingAngle_deg + y.HeadingAngle_deg;
        var pitchAngle_deg = x.PitchAngle_deg + y.PitchAngle_deg;
        var bankAngle_deg = x.BankAngle_deg + y.BankAngle_deg;

        var attitude = new Attitude(headingAngle_deg, pitchAngle_deg, bankAngle_deg);

        return attitude;
    }

    public static Attitude operator -(Attitude a1, Attitude a2)
    {
        var headingAngle_deg = a1.HeadingAngle_deg - a2.HeadingAngle_deg;
        var pitchAngle_deg = a1.PitchAngle_deg - a2.PitchAngle_deg;
        var bankAngle_deg = a1.BankAngle_deg - a2.BankAngle_deg;

        var attitude = new Attitude(headingAngle_deg, pitchAngle_deg, bankAngle_deg);

        return attitude;
    }

    public static AttitudeRate operator /(Attitude x, DeltaTime dt)
    {
        var headingAngleRate_deg = x.HeadingAngle_deg / dt.DeltaTime_s;
        var pitchAngleRate_deg = x.PitchAngle_deg / dt.DeltaTime_s;
        var bankAngleRate_deg = x.BankAngle_deg / dt.DeltaTime_s;

        var attitudeRate = new AttitudeRate(headingAngleRate_deg, pitchAngleRate_deg, bankAngleRate_deg);

        return attitudeRate;
    }

    public static Attitude operator *(Attitude x, double y)
    {
        var headingAngle_deg = x.HeadingAngle_deg * y;
        var pitchAngle_deg = x.PitchAngle_deg * y;
        var bankAngle_deg = x.BankAngle_deg * y;

        var attitude = new Attitude(headingAngle_deg, pitchAngle_deg, bankAngle_deg);

        return attitude;
    }

    public static Attitude operator *(double x, Attitude y)
    {
        var attitude = y * x;

        return attitude;
    }

    public static Attitude operator /(Attitude x, double y)
    {
        var headingAngle_deg = x.HeadingAngle_deg / y;
        var pitchAngle_deg = x.PitchAngle_deg / y;
        var bankAngle_deg = x.BankAngle_deg / y;

        var attitude = new Attitude(headingAngle_deg, pitchAngle_deg, bankAngle_deg);

        return attitude;
    }

    public static Attitude operator /(double x, Attitude y)
    {
        var attitude = y / x;

        return attitude;
    }

    public Matrix GetTransformationMatrix()
    {
        var t1 = CalculateTransformationMatrixHeading();
        var t2 = CalculateTransformationMatrixPitch();
        var t3 = CalculateTransformationMatrixBank();

        var t = t3 * t2 * t1;

        return t;
    }

    public Matrix GetTransformationMatrix_Inverse()
    {
        var headingAngle = HeadingAngle_deg.DegreesToRadians();
        var pitchAngle = PitchAngle_deg.DegreesToRadians();
        var bankAngle = BankAngle_deg.DegreesToRadians();

        var ct = Cos(headingAngle);
        var st = Sin(headingAngle);

        var cp = Cos(pitchAngle);
        var sp = Sin(pitchAngle);

        var cw = Cos(bankAngle);
        var sw = Sin(bankAngle);

        var data = new[,]
        {
            { ct * cp, ct * sp * sw - st * cw, ct * sp * cw + st * sw },
            { st* cp, st*sp * sw + ct * cw, st* sp*cw - ct * sw },
            { -sp,    cp* sw, cp*cw }
        };

        var t = new Matrix(data);

        return t;
    }

    public Matrix CalculateTransformationMatrixHeading()
    {
        var headingAngle = HeadingAngle_deg.DegreesToRadians();

        var ct = Cos(headingAngle);
        var st = Sin(headingAngle);

        var t = new Matrix(new double[,] { { ct, st, 0 }, { -st, ct, 0 }, { 0, 0, 1 } });

        return t;
    }

    public Matrix CalculateTransformationMatrixPitch()
    {
        var pitchAngle = PitchAngle_deg.DegreesToRadians();

        var cp = Cos(pitchAngle);
        var sp = Sin(pitchAngle);

        var t = new Matrix(new double[,] { { cp, 0, -sp }, { 0, 1, 0 }, { sp, 0, cp } });

        return t;
    }

    public Matrix CalculateTransformationMatrixBank()
    {
        var bankAngle = BankAngle_deg.DegreesToRadians();

        var ct = Cos(bankAngle);
        var st = Sin(bankAngle);

        var t = new Matrix(new double[,] { { 1, 0, 0 }, { 0, ct, st }, { 0, -st, ct } });

        return t;
    }
}