using static System.Math;

namespace MissionEngineering.MathLibrary;

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