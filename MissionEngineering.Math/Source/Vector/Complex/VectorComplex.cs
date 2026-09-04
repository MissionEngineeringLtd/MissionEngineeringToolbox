using System.Collections;
using System.Numerics;

namespace MissionEngineering.Math;

public partial class VectorComplex
{
    public int NumberOfElements => Data.Length;

    public Complex[] Data { get; set; }

    public VectorComplex()
    {
        Data = [];
    }

    public VectorComplex(int numberOfElements)
    {
        Data = new Complex[numberOfElements];
    }

    public VectorComplex(params double[] data)
    {
        Data = [.. data.Select(x => new Complex(x, 0.0))];
    }

    public VectorComplex(double[] real, double[] imag)
    {
        Data = [.. real.Zip(imag, (r, i) => new Complex(r, i))];
    }

    public VectorComplex(params Complex[] data)
    {
        Data = data;
    }

    public Complex this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public Complex this[Index index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public Complex[] this[Range index]
    {
        get => Data[index];
        set => Data = value;
    }

    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public VectorComplex Copy()
    {
        var result = new VectorComplex(NumberOfElements);

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
            var deltaX = (x.Data[i] - Data[i]).Magnitude;

            if (deltaX > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}