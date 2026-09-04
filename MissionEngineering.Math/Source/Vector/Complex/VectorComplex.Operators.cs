namespace MissionEngineering.Math;

public partial class VectorComplex
{
    public static VectorComplex operator +(VectorComplex left, double right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] + right;
        }

        return result;
    }

    public static VectorComplex operator +(double left, VectorComplex right)
    {
        var result = right + left;

        return result;
    }

    public static VectorComplex operator +(VectorComplex left, VectorComplex right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] + right.Data[i];
        }

        return result;
    }

    public static VectorComplex operator -(VectorComplex left)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = -left.Data[i];
        }

        return result;
    }

    public static VectorComplex operator -(VectorComplex left, VectorComplex right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] - right.Data[i];
        }

        return result;
    }

    public static VectorComplex operator *(VectorComplex left, double right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] * right;
        }

        return result;
    }

    public static VectorComplex operator *(double left, VectorComplex right)
    {
        var result = right * left;

        return result;
    }

    public static VectorComplex operator *(VectorComplex left, VectorComplex right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] * right.Data[i];
        }

        return result;
    }

    public static VectorComplex operator /(VectorComplex left, double right)
    {
        var result = new VectorComplex(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] / right;
        }

        return result;
    }

    public static VectorComplex operator /(double left, VectorComplex right)
    {
        var result = new VectorComplex(right.NumberOfElements);

        for (int i = 0; i < right.NumberOfElements; i++)
        {
            result.Data[i] = left / right.Data[i];
        }

        return result;
    }
}