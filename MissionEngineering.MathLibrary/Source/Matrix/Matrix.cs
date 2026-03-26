namespace MissionEngineering.MathLibrary;

public partial class Matrix
{
    public double[,] Data { get; set; }

    public int NumberOfRows => Data.GetLength(0);

    public int NumberOfColumns => Data.GetLength(1);

    public Matrix() : this(0, 0)
    {
    }

    public Matrix(int numberOfRows, int numberOfColumns)
    {
        Data = new double[numberOfRows, numberOfColumns];
    }

    public Matrix(double[,] data)
    {
        Data = data;
    }

    public double this[int rowIndex, int columnIndex]
    {
        get => Data[rowIndex, columnIndex];
        set => Data[rowIndex, columnIndex] = value;
    }

    public Matrix this[Range rowIndices, Range columnIndices]
    {
        get
        {
            var (rowOffset, rowLength) = rowIndices.GetOffsetAndLength(NumberOfRows);
            var (columnOffset, columnLength) = columnIndices.GetOffsetAndLength(NumberOfColumns);

            var subMatrix = new Matrix(rowLength, columnLength);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < columnLength; j++)
                {
                    subMatrix[i, j] = Data[rowOffset + i, columnOffset + j];
                }
            }

            return subMatrix;
        }
        set
        {
            var (rowOffset, rowLength) = rowIndices.GetOffsetAndLength(NumberOfRows);
            var (columnOffset, columnLength) = columnIndices.GetOffsetAndLength(NumberOfColumns);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < columnLength; j++)
                {
                    Data[rowOffset + i, columnOffset + j] = value[i, j];
                }
            }
        }
    }

    public Matrix this[int[] rowIndices, int[] columnIndices]
    {
        get
        {
            var nRows = rowIndices.Length;
            var nColumns = columnIndices.Length;

            var subMatrix = new Matrix(nRows, nColumns);

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    subMatrix[i, j] = Data[rowIndices[i], columnIndices[j]];
                }
            }

            return subMatrix;
        }
        set
        {
            var nRows = value.NumberOfRows;
            var nColumns = value.NumberOfColumns;

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    Data[rowIndices[i], columnIndices[j]] = value[i, j];
                }
            }
        }
    }

    public bool Equals(Matrix x, double tolerance = 1.0e-9)
    {
        if (this is null)
        {
            return false;
        }

        if (x is null)
        {
            return false;
        }

        if (x.NumberOfRows != NumberOfRows)
        {
            return false;
        }

        if (x.NumberOfColumns != NumberOfColumns)
        {
            return false;
        }

        var deltaX = 0.0;

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                deltaX = Math.Abs(x.Data[i, j] - Data[i, j]);

                if (deltaX > tolerance)
                {
                    return false;
                }
            }
        }

        return true;
    }
}