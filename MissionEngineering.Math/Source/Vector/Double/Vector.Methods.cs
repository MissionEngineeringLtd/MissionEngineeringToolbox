using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Optimization;

namespace MissionEngineering.Math;

public partial class Vector
{
    public Vector(PositionNED positionNED, VelocityNED velocityNED)
    {
        Data = new double[6];

        Data[0] = positionNED.PositionNorth_m;
        Data[1] = positionNED.PositionEast_m;
        Data[2] = positionNED.PositionDown_m;
        Data[3] = velocityNED.VelocityNorth_ms;
        Data[4] = velocityNED.VelocityEast_ms;
        Data[5] = velocityNED.VelocityDown_ms;
    }

    public Vector(PositionNED positionNED, VelocityNED velocityNED, AccelerationNED accelerationNED)
    {
        Data = new double[9];

        Data[0] = positionNED.PositionNorth_m;
        Data[1] = positionNED.PositionEast_m;
        Data[2] = positionNED.PositionDown_m;
        Data[3] = velocityNED.VelocityNorth_ms;
        Data[4] = velocityNED.VelocityEast_ms;
        Data[5] = velocityNED.VelocityDown_ms;
        Data[6] = accelerationNED.AccelerationNorth_ms2;
        Data[7] = accelerationNED.AccelerationEast_ms2;
        Data[8] = accelerationNED.AccelerationDown_ms2;
    }

    public double Norm()
    {
        double sum = DotProduct(this, this);

        return System.Math.Sqrt(sum);
    }

    public Vector Sqrt()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = System.Math.Sqrt(Data[i]);
        }

        return result;
    }

    public Vector UnitVector()
    {
        var result = this / Norm();

        return result;
    }

    public double DotProduct(Vector x)
    {
        var result = DotProduct(this, x);

        return result;
    }

    public Vector CrossProduct(Vector x)
    {
        var result = CrossProduct(this, x);

        return result;
    }

    public double AngleBetweenVectors_rad(Vector x)
    {
        var angle_rad = AngleBetweenVectors_rad(this, x);

        return angle_rad;
    }

    public double AngleBetweenVectors_deg(Vector x)
    {
        var angle_deg = AngleBetweenVectors_deg(this, x);

        return angle_deg;
    }

    public Vector Abs()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = System.Math.Abs(Data[i]);
        }

        return result;
    }

    public Vector PadRight(int totalNumberOfElements, double padValue = 0.0)
    {
        var result = new Vector(totalNumberOfElements);

        for (int i = 0; i < totalNumberOfElements; i++)
        {
            if (i < NumberOfElements)
            {
                result[i] = Data[i];
            }
            else
            {
                result[i] = padValue;
            }
        }

        return result;
    }

    public Vector PowerToDecibels()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = 10.0 * System.Math.Log10(Data[i]);
        }

        return result;
    }

    public Vector DecibelsToPower()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = System.Math.Pow(10.0, Data[i] / 10.0);
        }

        return result;
    }

    public Vector FFT()
    {
        var reData = Data;
        var imData = new double[NumberOfElements];

        Fourier.Forward(reData, imData, FourierOptions.Matlab);

        var y = new Vector(reData);

        return y;
    }

    public double Max()
    {
        var result = Data.Max();

        return result;
    }
}
