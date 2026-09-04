using System.Collections;
using static System.Math;

namespace MissionEngineering.Math;

public partial class Vector
{
    public int NumberOfElements => Data.Length;

    public double[] Data { get; set; }

    public Vector()
    {
        Data = [];
    }

    public Vector(int numberOfElements)
    {
        Data = new double[numberOfElements];
    }

    public Vector(params double[] data)
    {
        Data = data;
    }



    public double this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public double this[Index index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public double[] this[Range index]
    {
        get => Data[index];
        set => Data = value;
    }

    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public Vector Copy()
    {
        var result = new Vector(NumberOfElements);

        Array.Copy(Data, result.Data, NumberOfElements);

        return result;
    }

    public bool Equals(Vector x, double tolerance = 1.0e-9)
    {
        if (this is null)
        {
            return false;
        }

        if (x is null)
        {
            return false;
        }

        if (x.NumberOfElements != NumberOfElements)
        {
            return false;
        }

        for (int i = 0; i < Data.Length; i++)
        {
            var deltaX = System.Math.Abs(x.Data[i] - Data[i]);

            if (deltaX > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}