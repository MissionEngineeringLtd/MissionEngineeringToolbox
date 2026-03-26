using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class MappingConversions
{
    public static PositionLLA ConvertPositionNEDToPositionLLA(PositionNED positionNED, PositionLLA positionLLAOrigin)
    {
        var R = PhysicalConstants.EarthRadiusEquatorial;
        var f = PhysicalConstants.EarthFlatteningFactor;

        var u0 = positionLLAOrigin.Latitude_deg.DegreesToRadians();
        var l0 = positionLLAOrigin.Longitude_deg.DegreesToRadians();

        var dN = positionNED.PositionNorth_m;
        var dE = positionNED.PositionEast_m;

        var numerator = 1 - (2 * f - f * f);
        var denominator = 1 - (2 * f - f * f) * Pow(Sin(u0), 2);

        var Rn = R / Sqrt(denominator);
        var Rm = Rn * numerator / denominator;

        var du = Atan2(1, Rm) * dN;
        var dl = Atan2(1, (Rn * Cos(u0))) * dE;

        var altitudeOrigin = positionLLAOrigin.Altitude_m;
        var altitude = -positionNED.PositionDown_m;

        var u = u0 + du;
        var l = l0 + dl;
        var d = altitude - altitudeOrigin;

        var u_deg = u.RadiansToDegrees();
        var l_deg = l.RadiansToDegrees();

        var positionLLA = new PositionLLA()
        {
            Latitude_deg = u_deg,
            Longitude_deg = l_deg,
            Altitude_m = d
        };

        return positionLLA;
    }

    public static PositionNED ConvertPositionLLAToPositionNED(PositionLLA positionLLA, PositionLLA positionLLAOrigin)
    {
        var R = PhysicalConstants.EarthRadiusEquatorial;
        var f = PhysicalConstants.EarthFlatteningFactor;

        var u0 = positionLLAOrigin.Latitude_deg.DegreesToRadians();
        var l0 = positionLLAOrigin.Longitude_deg.DegreesToRadians();

        var numerator = 1 - (2 * f - f * f);
        var denominator = 1 - (2 * f - f * f) * Pow(Sin(u0), 2);

        var Rn = R / Sqrt(denominator);
        var Rm = Rn * numerator / denominator;

        var u = positionLLA.Latitude_deg.DegreesToRadians();
        var l = positionLLA.Longitude_deg.DegreesToRadians();

        var du = u - u0;
        var dl = l - l0;

        var positionNorth_m = du / Atan2(1, Rm);
        var positionEast_m = dl / Atan2(1, (Rn * Cos(u0)));
        var positionDown_m = -(positionLLA.Altitude_m - positionLLAOrigin.Altitude_m);

        var positionNED = new PositionNED()
        {
            PositionNorth_m = positionNorth_m,
            PositionEast_m = positionEast_m,
            PositionDown_m = positionDown_m
        };

        return positionNED;
    }
}