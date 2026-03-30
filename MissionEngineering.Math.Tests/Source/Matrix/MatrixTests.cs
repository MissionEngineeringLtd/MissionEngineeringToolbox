namespace MissionEngineering.Math.Tests;

[TestClass]
public class MatrixTests
{
    [TestMethod]
    public void IndexerRange_WithValidData_ExpectSuccess()
    {
        // Arrange
        var a = Matrix.IdentityMatrix(3, 6);

        // Act
        var subMatrix = a[0..3, 0..3];

        // Assert
        Assert.AreEqual(3, subMatrix.NumberOfRows);
        Assert.AreEqual(3, subMatrix.NumberOfColumns);
    }

    [TestMethod]
    public void IndexerDouble_WithValidData_ExpectSuccess()
    {
        // Arrange
        var a = Matrix.IdentityMatrix(6, 6);

        var b = new Matrix(new double[2, 2] { { 1.0, 2.0 }, { 3.0, 4.0 } });

        // Act
        a[[1, 3], [1, 4]] = b;

        // Assert
        Assert.AreEqual(a[3, 4], 4.0);
    }

    [TestMethod]
    public void Inverse_WithValidData_ExpectSuccess()
    {
        // Arrange
        var a = new Matrix(new double[2, 2] { { 1.0, 2.0 }, { 3.0, 4.0 } });

        // Act
        var aInverse = a.Inverse();

        // Analyse
        var b = a * aInverse;

        var i = Matrix.IdentityMatrix(2, 2);

        var isEqual = b.Equals(i);

        // Assert
        Assert.IsTrue(isEqual, "The product of a and its inverse should be equal to the identity matrix.");
    }
}