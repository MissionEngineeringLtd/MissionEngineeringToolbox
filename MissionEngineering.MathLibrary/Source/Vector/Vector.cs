namespace MissionEngineering.MathLibrary;

public partial class Vector
{
    public int NumberOfElements => Data.Length;

    public double[] Data { get; set; }

    public Vector()
    {
        Data = Array.Empty<double>();
    }

    public Vector(int numberOfElements)
    {
        Data = new double[numberOfElements];
    }

    public Vector(params double[] data)
    {
        Data = data;
    }
}
