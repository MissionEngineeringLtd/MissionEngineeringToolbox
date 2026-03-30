namespace MissionEngineering.Math.Tests;

[TestClass]
public sealed class Interpolation1DTests
{
    [TestMethod]
    public void Create_WithEmptyArray_ExpectException()
    {
        // Arrange
        var x = new double[0];
        var y = new double[1];

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var interpolation = new Interpolation1D(x, y);
        });
    }

    [TestMethod]
    public void Create_WithDifferentSizedArrays_ExpectException()
    {
        // Arrange
        var x = new double[3];
        var y = new double[4];

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var interpolation = new Interpolation1D(x, y);
        });
    }

    [TestMethod]
    public void Create_WithValidSizes_ExpectSuccess()
    {
        // Arrange
        var x = new double[4];
        var y = new double[4];

        // Act
        var interpolation = new Interpolation1D(x, y);

        // Assert
        var isEqual = interpolation.X.NumberOfElements == interpolation.Y.NumberOfElements;

        Assert.IsTrue(isEqual);
    }

    [TestMethod]
    public void Interpolate_WithOneElement_ExpectSuccess()
    {
        // Arrange
        var x = new Vector(1.0);

        var y = new Vector(11.0);

        var interpolation = new Interpolation1D(x, y);

        var xi = 2.5;

        // Act
        var yi = interpolation.Interpolate(xi);

        // Assert
        Assert.AreEqual(11.0, yi, 1.0e-6);
    }

    [TestMethod]
    public void Interpolate_WithSingleValue_ExpectSuccess()
    {
        // Arrange
        var x = new Vector(1.0, 2.0, 3.0);

        var y = new Vector(11.0, 12.0, 22.0);

        var interpolation = new Interpolation1D(x, y);

        var xi = 2.5;

        // Act
        var yi = interpolation.Interpolate(xi);

        // Assert
        Assert.AreEqual(17.0, yi, 1.0e-6);
    }

    [TestMethod]
    public void Interpolate_WithMultipleValues_ExpectSuccess()
    {
        // Arrange
        var x = new Vector(1.0, 2.0, 3.0);

        var y = new Vector(11.0, 12.0, 22.0);

        var interpolation = new Interpolation1D(x, y);

        var xi = new Vector(-0.5, 1.0, 1.5, 2.0, 2.75, 3.0, 3.5);
        var yiExpected = new Vector(11.0, 11.0, 11.5, 12.0, 19.5, 22.0, 22.0);

        // Act
        var yi = interpolation.Interpolate(xi);

        // Assert
        var isEqual = yi.Equals(yiExpected, 1.0e-6);

        Assert.IsTrue(isEqual);
    }
}