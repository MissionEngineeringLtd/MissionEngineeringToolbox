namespace MissionEngineering.Math.Tests;

[TestClass]
public class VectorTests
{
    [TestMethod]
    public void DefaultConstructor_CreatesEmptyVector()
    {
        var vector = new Vector();
        Assert.IsNotNull(vector.Data);
        Assert.AreEqual(0, vector.NumberOfElements);
    }

    [TestMethod]
    public void Constructor_WithNumberOfElements_CreatesVectorWithCorrectLength()
    {
        int length = 5;
        var vector = new Vector(length);
        Assert.IsNotNull(vector.Data);
        Assert.AreEqual(length, vector.NumberOfElements);
        foreach (var value in vector.Data)
        {
            Assert.AreEqual(0.0, value);
        }
    }

    [TestMethod]
    public void Constructor_WithDataArray_InitializesData()
    {
        double[] data = { 1.1, 2.2, 3.3 };
        var vector = new Vector(data);
        Assert.AreEqual(data.Length, vector.NumberOfElements);
        CollectionAssert.AreEqual(data, vector.Data);
    }

    [TestMethod]
    public void Indexer_Int_GetAndSet_WorksCorrectly()
    {
        var vector = new Vector(3);
        vector[0] = 10.5;
        vector[1] = 20.5;
        vector[2] = 30.5;

        Assert.AreEqual(10.5, vector[0]);
        Assert.AreEqual(20.5, vector[1]);
        Assert.AreEqual(30.5, vector[2]);
    }

    [TestMethod]
    public void Indexer_Index_GetAndSet_WorksCorrectly()
    {
        var vector = new Vector(new double[] { 5.5, 6.6, 7.7 });
        vector[new Index(1)] = 99.9;
        Assert.AreEqual(99.9, vector[new Index(1)]);
        Assert.AreEqual(5.5, vector[new Index(0)]);
        Assert.AreEqual(7.7, vector[new Index(2)]);
    }

    [TestMethod]
    public void Indexer_Int_ThrowsOnInvalidIndex()
    {
        var vector = new Vector(2);

        Assert.Throws<IndexOutOfRangeException>(() => vector[2]);
    }

    [TestMethod]
    public void Indexer_Index_ThrowsOnInvalidIndex()
    {
        var vector = new Vector(2);

        Assert.Throws<IndexOutOfRangeException>(() => vector[new Index(2)]);
    }
}