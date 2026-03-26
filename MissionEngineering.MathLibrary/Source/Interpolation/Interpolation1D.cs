using CommunityToolkit.Diagnostics;

namespace MissionEngineering.MathLibrary;

public class Interpolation1D
{
    public Vector X { get; set; }

    public Vector Y { get; set; }

    private readonly double xStart;

    private readonly double xEnd;

    private readonly double yStart;

    private readonly double yEnd;

    private readonly double xStep;

    public Interpolation1D(Vector x, Vector y)
    {
        Guard.IsNotNull(x, nameof(x));
        Guard.IsNotNull(y, nameof(y));

        Guard.IsEqualTo(y.NumberOfElements, x.NumberOfElements, nameof(y));

        X = x;
        Y = y;

        (xStart, xEnd, yStart, yEnd, xStep) = GetEndPoints();
    }

    public Interpolation1D(double[] x, double[] y) : this(new Vector(x), new Vector(y))
    {
    }

    private (double xStart, double xEnd, double yStart, double yEnd, double xStep) GetEndPoints()
    {
        var xStart = X[0];
        var xEnd = X[^1];
        var yStart = Y[0];
        var yEnd = Y[^1];

        var numberOfElements = X.NumberOfElements;

        var xStep = 1.0;

        if (numberOfElements > 1)
        {
            xStep = (xEnd - xStart) / (X.NumberOfElements - 1);
        }

        return (xStart, xEnd, yStart, yEnd, xStep);
    }

    public double Interpolate(double xi)
    {
        if (xi <= xStart)
        {
            return yStart;
        }
        else if (xi >= xEnd)
        {
            return yEnd;
        }

        var xIndex = (int)((xi - xStart) / xStep);

        var x0 = X[xIndex];
        var x1 = X[xIndex + 1];

        var y0 = Y[xIndex];
        var y1 = Y[xIndex + 1];

        var yi = LinearInterpolate(x0, x1, y0, y1, xi);

        return yi;
    }

    public double[] Interpolate(double[] x)
    {
        var y = new double[x.Length];

        for (int i = 0; i < x.Length; i++)
        {
            y[i] = Interpolate(x[i]);
        }

        return y;
    }

    public double[] Interpolate(Span<double> x)
    {
        var y = new double[x.Length];

        for (int i = 0; i < x.Length; i++)
        {
            y[i] = Interpolate(x[i]);
        }

        return y;
    }

    public Vector Interpolate(Vector x)
    {
        var y = Interpolate(x.Data);

        var result = new Vector(y);

        return result;
    }

    private double LinearInterpolate(double x0, double x1, double y0, double y1, double xi)
    {
        var yi = (y0 * (x1 - xi) + y1 * (xi - x0)) / xStep;

        return yi;
    }
}