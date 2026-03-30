using static System.Math;

namespace MissionEngineering.Math;

public partial class Matrix
{
    public static Matrix IdentityMatrix(int numberOfRows, int numberOfColumns)
    {
        Matrix x = new Matrix(numberOfRows, numberOfColumns);

        var numberOElements = Min(numberOfRows, numberOfColumns);

        for (int i = 0; i < numberOElements; i++)
        {
            x[i, i] = 1.0;
        }

        return x;
    }

    public Vector Diagonal()
    {
        var numberOfElements = Min(NumberOfRows, NumberOfColumns);

        var diagonal = new Vector(NumberOfRows);

        for (int i = 0; i < NumberOfRows; i++)
        {
            diagonal[i] = this[i, i];
        }

        return diagonal;
    }
}