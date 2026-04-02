namespace MissionEngineering.Math.Tests;

[TestClass]
public class DoubleExtensionMethodsTests
{
    private const double Tolerance = 1e-9;

    [TestMethod]
    public void PowerAndDecibel_RoundTrip()
    {
        var power = 2.0;
        var db = power.PowerToDecibels();
        var powerBack = db.DecibelsToPower();
        Assert.AreEqual(power, powerBack, Tolerance);
    }

    [TestMethod]
    public void Angle_RadiansDegrees_RoundTrip()
    {
        var radians = 1.234;
        var deg = radians.RadiansToDegrees();
        var radBack = deg.DegreesToRadians();
        Assert.AreEqual(radians, radBack, Tolerance);
    }

    [TestMethod]
    public void Length_MetersFeet_RoundTrip()
    {
        var meters = 10.0;
        var feet = meters.MetersToFeet();
        var metersBack = feet.FeetToMeters();
        Assert.AreEqual(meters, metersBack, Tolerance);
    }

    [TestMethod]
    public void Length_MetersKilometers_RoundTrip()
    {
        var meters = 1500.0;
        var km = meters.MetersToKilometers();
        var metersBack = km.KilometersToMeters();
        Assert.AreEqual(meters, metersBack, Tolerance);
    }

    [TestMethod]
    public void Length_MetersNauticalMiles_RoundTrip()
    {
        var meters = 3704.0; // exactly 2 nautical miles
        var nm = meters.MetersToNauticalMiles();
        var metersBack = nm.NauticalMilesToMeters();
        Assert.AreEqual(meters, metersBack, 1e-6);
    }

    [TestMethod]
    public void Speed_MetersPerSecondKnots_RoundTrip()
    {
        var mps = 10.0;
        var knots = mps.MetersPerSecondToKnots();
        var mpsBack = knots.KnotsToMetersPerSecond();
        Assert.AreEqual(mps, mpsBack, 1e-9);
    }

    [TestMethod]
    public void FrequencyWavelength_RoundTrip()
    {
        var freq = 1.0e9; // 1 GHz
        var lambda = freq.FrequencyToWavelength();
        var freqBack = lambda.WavelengthToFrequency();
        Assert.AreEqual(freq, freqBack, 1e-6);
    }

    [TestMethod]
    public void Acceleration_G_RoundTrip()
    {
        var accel = 9.80665;
        var g = accel.MetersPerSecondSquaredToG();
        var accelBack = g.GToMetersPerSecondSquared();
        Assert.AreEqual(accel, accelBack, 1e-9);
    }

    [TestMethod]
    public void RpmRadians_RoundTrip()
    {
        var rad = 1.234;
        var rpm = rad.RadiansToRpm();
        var radBack = rpm.RpmToRadians();
        Assert.AreEqual(rad, radBack, Tolerance);
    }

    [TestMethod]
    public void DegreesRpm_RoundTrip()
    {
        var degrees = 720.0;
        var rpm = degrees.DegreesToRpm();
        var degreesBack = rpm.RpmToDegrees();
        Assert.AreEqual(degrees, degreesBack, Tolerance);
    }

    [TestMethod]
    public void Time_SecondsMillisecondsMicrosecondsNanoseconds_RoundTrips()
    {
        var s = 1.234567;
        Assert.AreEqual(s, s.SecondsToMilliseconds().MillisecondsToSeconds(), Tolerance);
        Assert.AreEqual(s, s.SecondsToMicroseconds().MicrosecondsToSeconds(), 1e-12);
        Assert.AreEqual(s, s.SecondsToNanoseconds().NanosecondsToSeconds(), 1e-6);
    }

    [TestMethod]
    public void ConstrainAngle0To2PI_WrapsAboveAndBelow()
    {
        var above = 7.0; // > 2*pi
        var expectedAbove = above - 2.0 * System.Math.PI;
        Assert.AreEqual(expectedAbove, above.ConstrainAngle0To2PI(), Tolerance);

        var below = -1.0;
        var expectedBelow = below + 2.0 * System.Math.PI;
        Assert.AreEqual(expectedBelow, below.ConstrainAngle0To2PI(), Tolerance);
    }

    [TestMethod]
    public void ConstrainAnglePlusMinusPI_WrapsAbove()
    {
        var above = 4.0; // > pi
        var expected = above - 2.0 * System.Math.PI;
        Assert.AreEqual(expected, above.ConstrainAnglePlusMinusPI(), Tolerance);
    }

    [TestMethod]
    public void ConstrainAngle0To360_WrapsAboveAndBelow()
    {
        var above = 370.0;
        Assert.AreEqual(10.0, above.ConstrainAngle0To360(), Tolerance);

        var below = -10.0;
        Assert.AreEqual(-10.0 + 360.0, below.ConstrainAngle0To360(), Tolerance);
    }

    [TestMethod]
    public void ConstrainAnglePlusMinus180_WrapsAboveAndBelow()
    {
        var above = 190.0;
        Assert.AreEqual(-170.0, above.ConstrainAnglePlusMinus180(), Tolerance);

        var below = -190.0;
        Assert.AreEqual(170.0, below.ConstrainAnglePlusMinus180(), Tolerance);
    }
}