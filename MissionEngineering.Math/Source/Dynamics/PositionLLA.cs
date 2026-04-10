namespace MissionEngineering.Math;

public record PositionLLA
{
    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }

    public double Altitude_m { get; set; }

    public PositionLLA()
    {
    }

    public PositionLLA(double latitude_deg, double longitude_deg, double altitude_m)
    {
        Latitude_deg = latitude_deg;
        Longitude_deg = longitude_deg;
        Altitude_m = altitude_m;
    }
}