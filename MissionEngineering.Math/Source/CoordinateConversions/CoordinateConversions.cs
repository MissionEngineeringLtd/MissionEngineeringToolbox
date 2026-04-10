using static System.Math;

namespace MissionEngineering.Math;

public static class CoordinateConversions
{
    public static Polars CartesiansToPolars(PositionNED positionNED)
    {
        var cartesians = new Cartesians(positionNED.PositionNorth_m, positionNED.PositionEast_m, positionNED.PositionDown_m, 0.0, 0.0, 0.0);

        var polars = CartesiansToPolars(cartesians);

        return polars;
    }

    public static Polars CartesiansToPolars(PositionNED positionNED, VelocityNED velocityNED)
    {
        var cartesians = new Cartesians(positionNED.PositionNorth_m, positionNED.PositionEast_m, positionNED.PositionDown_m, velocityNED.VelocityNorth_ms, velocityNED.VelocityEast_ms, velocityNED.VelocityDown_ms);

        var polars = CartesiansToPolars(cartesians);

        return polars;
    }

    public static (PositionNED, VelocityNED) PolarsToPositionVelocityNED(Polars polars)
    {
        var cartesians = PolarsToCartesians(polars);

        var positionNED = new PositionNED(cartesians.PositionX, cartesians.PositionY, cartesians.PositionZ);
        var velocityNED = new VelocityNED(cartesians.VelocityX, cartesians.VelocityY, cartesians.VelocityZ);

        return (positionNED, velocityNED);
    }

    public static (double, double) CalculateAspectAngles(VelocityNED relativeVelocityLOS)
    {
        var relativeVelocityLOSAttitude = FrameConversions.GetAttitudeFromVelocityVector(relativeVelocityLOS);

        var aspectAngleAzimuth_deg = (180.0 - relativeVelocityLOSAttitude.HeadingAngle_deg).ConstrainAnglePlusMinus180();
        var aspectAngleElevation_deg = -relativeVelocityLOSAttitude.PitchAngle_deg;

        return (aspectAngleAzimuth_deg, aspectAngleElevation_deg);
    }

    public static Polars CartesiansToPolars(Cartesians cartesians)
    {
        var x = cartesians.PositionX;
        var y = cartesians.PositionY;
        var z = cartesians.PositionZ;

        var vx = cartesians.VelocityX;
        var vy = cartesians.VelocityY;
        var vz = cartesians.VelocityZ;

        var r = Sqrt(x * x + y * y + z * z);
        var phi = Atan2(y, x);
        var theta = Asin(-z / r);

        var cp = Cos(phi);
        var sp = Sin(phi);
        var ct = Cos(theta);
        var st = Sin(theta);

        var rDot = (x * vx + y * vy + z * vz) / r;
        var phiDot = (vy * cp - vx * sp) / (r * ct);
        var thetaDot = -(vz * ct + (vx * cp + vy * sp) * st) / r;

        var polars = new Polars(r, rDot, phi, phiDot, theta, thetaDot);

        return polars;
    }

    public static Cartesians PolarsToCartesians(Polars polars)
    {
        var r = polars.Range_m;
        var rDot = polars.RangeRate_ms;
        var phi = polars.AzimuthAngle_rad;
        var phiDot = polars.AzimuthRate_rads;
        var theta = polars.ElevationAngle_rad;
        var thetaDot = polars.ElevationRate_rads;

        var cp = Cos(phi);
        var sp = Sin(phi);
        var ct = Cos(theta);
        var st = Sin(theta);

        var x = r * cp * ct;
        var y = r * sp * ct;
        var z = -r * st;

        var vx = rDot * cp * ct - r * sp * ct * phiDot - r * cp * st * thetaDot;
        var vy = rDot * sp * ct + r * cp * ct * phiDot - r * sp * st * thetaDot;
        var vz = -rDot * st - r * ct * thetaDot;

        var cartesians = new Cartesians(x, y, z, vx, vy, vz);

        return cartesians;
    }

    public static Matrix JacobianCartesiansWrtPolars(Polars polars)
    {
        var r = polars.Range_m;
        var rDot = polars.RangeRate_ms;
        var phi = polars.AzimuthAngle_rad;
        var phiDot = polars.AzimuthRate_rads;
        var theta = polars.ElevationAngle_rad;
        var thetaDot = polars.ElevationRate_rads;

        var cp = Cos(phi);
        var sp = Sin(phi);
        var ct = Cos(theta);
        var st = Sin(theta);

        var j = new double[6, 6];

        j[0, 0] = cp * ct;
        j[0, 1] = 0;
        j[0, 2] = -r * sp * ct;
        j[0, 3] = 0;
        j[0, 4] = -r * cp * st;
        j[0, 5] = 0;

        j[1, 0] = sp * ct;
        j[1, 1] = 0;
        j[1, 2] = r * cp * ct;
        j[1, 3] = 0;
        j[1, 4] = -r * sp * st;
        j[1, 5] = 0;

        j[2, 0] = -st;
        j[2, 1] = 0;
        j[2, 2] = 0;
        j[2, 3] = 0;
        j[2, 4] = -r * ct;
        j[2, 5] = 0;

        j[3, 0] = -sp * ct * phiDot - cp * st * thetaDot;
        j[3, 1] = cp * ct;
        j[3, 2] = -rDot * sp * ct - r * cp * ct * phiDot + r * sp * st * thetaDot;
        j[3, 3] = -r * sp * ct;
        j[3, 4] = -rDot * cp * st + r * sp * st * phiDot - r * cp * ct * thetaDot;
        j[3, 5] = -r * cp * st;

        j[4, 0] = +cp * ct * phiDot - sp * st * thetaDot;
        j[4, 1] = sp * ct;
        j[4, 2] = rDot * cp * ct - r * sp * ct * phiDot - r * cp * st * thetaDot;
        j[4, 3] = +r * cp * ct;
        j[4, 4] = -rDot * sp * st - r * cp * st * phiDot - r * sp * ct * thetaDot;
        j[4, 5] = -r * sp * st;

        j[5, 0] = -ct * thetaDot;
        j[5, 1] = -st;
        j[5, 2] = 0;
        j[5, 3] = 0;
        j[5, 4] = -rDot * ct + r * st * thetaDot;
        j[5, 5] = -r * ct;

        var result = new Matrix(j);

        return result;
    }

    public static Matrix JacobianPolarsWrtCartesians(Cartesians cartesians)
    {
        var polars = CartesiansToPolars(cartesians);

        var x = cartesians.PositionX;
        var y = cartesians.PositionY;
        var z = cartesians.PositionZ;

        var vx = cartesians.VelocityX;
        var vy = cartesians.VelocityY;
        var vz = cartesians.VelocityZ;

        var r = polars.Range_m;
        var rDot = polars.RangeRate_ms;
        var phi = polars.AzimuthAngle_rad;
        var phiDot = polars.AzimuthRate_rads;
        var theta = polars.ElevationAngle_rad;
        var thetaDot = polars.ElevationRate_rads;

        var rg = r * Cos(theta);

        var rSquared = r * r;
        var rgSquared = rg * rg;

        var j = new double[6, 6];

        j[0, 0] = x / r;
        j[0, 1] = y / r;
        j[0, 2] = z / r;
        j[0, 3] = 0.0;
        j[0, 4] = 0.0;
        j[0, 5] = 0.0;

        j[1, 0] = (r * vx - rDot * x) / rSquared;
        j[1, 1] = (r * vy - rDot * y) / rSquared;
        j[1, 2] = (r * vz - rDot * z) / rSquared;
        j[1, 3] = x / r;
        j[1, 4] = y / r;
        j[1, 5] = z / r;

        j[2, 0] = -y / rgSquared;
        j[2, 1] = x / rgSquared;
        j[2, 2] = 0.0;
        j[2, 3] = 0.0;
        j[2, 4] = 0.0;
        j[2, 5] = 0.0;

        j[3, 0] = (vy - 2 * x * phiDot) / rgSquared;
        j[3, 1] = (-vx - 2 * y * phiDot) / rgSquared;
        j[3, 2] = 0.0;
        j[3, 3] = -y / rgSquared;
        j[3, 4] = x / rgSquared;
        j[3, 5] = 0.0;

        j[4, 0] = (x * z) / (rg * rSquared);
        j[4, 1] = (y * z) / (rg * rSquared);
        j[4, 2] = -rg / rSquared;
        j[4, 3] = 0.0;
        j[4, 4] = 0.0;
        j[4, 5] = 0.0;

        j[5, 0] = (j[1, 0] - (rDot * j[0, 0] / r)) * z / (r * rg) - thetaDot * x / rgSquared;
        j[5, 1] = (j[1, 1] - (rDot * j[0, 1] / r)) * z / (r * rg) - thetaDot * y / rgSquared;
        j[5, 2] = (j[1, 2] - (rDot * j[0, 2] / r)) * z / (r * rg) + rDot / (r * rg);
        j[5, 3] = j[4, 0];
        j[5, 4] = j[4, 1];
        j[5, 5] = j[4, 2];

        var result = new Matrix(j);

        return result;
    }
}