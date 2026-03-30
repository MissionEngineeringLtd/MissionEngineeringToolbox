namespace MissionEngineering.Math;

public partial class Vector
{
    public double Norm()
    {
        double sum = 0.0;

        for (int i = 0; i < NumberOfElements; i++)
        {
            sum += Data[i] * Data[i];
        }

        return System.Math.Sqrt(sum);
    }

    public Vector Sqrt()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = System.Math.Sqrt(Data[i]);
        }

        return result;
    }
}