using static System.Math;

namespace MissionEngineering.Math;

public record Polars
{
    public double Range_m { get; init; }

    public double RangeRate_ms { get; init; }

    public double AzimuthAngle_rad { get; init; }

    public double AzimuthRate_rads { get; init; }

    public double ElevationAngle_rad { get; init; }

    public double ElevationRate_rads { get; init; }

    public double GroundRange => Range_m * Cos(ElevationAngle_rad);

    public Polars()
    {
    }

    public Polars(double range_m, double rangeRate_ms, double azimuthAngle_rad, double azimuthRate_rads, double elevationAngle_rad, double elevationRate_rads)
    {
        Range_m = range_m;
        RangeRate_ms = rangeRate_ms;
        AzimuthAngle_rad = azimuthAngle_rad;
        AzimuthRate_rads = azimuthRate_rads;
        ElevationAngle_rad = elevationAngle_rad;
        ElevationRate_rads = elevationRate_rads;
    }

    public Polars(double[] polars)
    {
        Range_m = polars[0];
        RangeRate_ms = polars[1];
        AzimuthAngle_rad = polars[2];
        AzimuthRate_rads = polars[3];
        ElevationAngle_rad = polars[4];
        ElevationRate_rads = polars[5];
    }

    public Polars(Vector polars) : this(polars.Data)
    {
    }

    public Cartesians ToCartesians()
    {
        var cartesians = CoordinateConversions.PolarsToCartesians(this);

        return cartesians;
    }

    public Matrix JacobianCartesiansWrtPolars()
    {
        var j = CoordinateConversions.JacobianCartesiansWrtPolars(this);

        return j;
    }
}