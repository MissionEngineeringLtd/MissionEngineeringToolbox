namespace MissionEngineering.MathLibrary;

public interface IInterpolation1D
{
    public Vector X { get; init; }

    public Vector Y { get; init; }

    double Interpolate(double x);

    double[] Interpolate(double[] x);

    double[] Interpolate(Span<double> x);

    Vector Interpolate(Vector x);
}