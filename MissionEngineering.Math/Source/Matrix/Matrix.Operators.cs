namespace MissionEngineering.MathLibrary;

public partial class Matrix
{
    public static Matrix operator +(Matrix left, Matrix right)
    {
        Matrix result = new Matrix(left.NumberOfRows, right.NumberOfColumns);

        for (int i = 0; i < left.NumberOfRows; i++)
        {
            for (int j = 0; j < right.NumberOfColumns; j++)
            {
                result[i, j] = left[i, j] + right[i, j];
            }
        }

        return result;
    }

    public static Matrix operator -(Matrix left, Matrix right)
    {
        Matrix result = new Matrix(left.NumberOfRows, right.NumberOfColumns);

        for (int i = 0; i < left.NumberOfRows; i++)
        {
            for (int j = 0; j < right.NumberOfColumns; j++)
            {
                result[i, j] = left[i, j] - right[i, j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix left, double right)
    {
        Matrix result = new Matrix(left.NumberOfRows, left.NumberOfColumns);

        for (int i = 0; i < left.NumberOfRows; i++)
        {
            for (int j = 0; j < left.NumberOfColumns; j++)
            {
                result[i, j] = left[i, j] * right;
            }
        }

        return result;
    }

    public static Matrix operator *(double left, Matrix right)
    {
        return right * left;
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
        Matrix result = new Matrix(left.NumberOfRows, right.NumberOfColumns);

        for (int i = 0; i < left.NumberOfRows; i++)
        {
            for (int j = 0; j < right.NumberOfColumns; j++)
            {
                for (int k = 0; k < left.NumberOfColumns; k++)
                {
                    result[i, j] += left[i, k] * right[k, j];
                }
            }
        }

        return result;
    }

    public static Vector operator *(Matrix left, Vector right)
    {
        Vector result = new Vector(left.NumberOfRows);

        for (int i = 0; i < left.NumberOfRows; i++)
        {
            for (int j = 0; j < left.NumberOfColumns; j++)
            {
                result[i] += left[i, j] * right[j];
            }
        }

        return result;
    }

    public Matrix Transpose()
    {
        Matrix result = new Matrix(NumberOfColumns, NumberOfRows);

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                result[j, i] = this[i, j];
            }
        }

        return result;
    }

    public Matrix Inverse()
    {
        var m = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(Data);

        var mInverse = m.Inverse();

        var result = new Matrix(mInverse.ToArray());

        return result;
    }
}