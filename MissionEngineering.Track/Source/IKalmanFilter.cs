using MissionEngineering.Math;

namespace MissionEngineering.Track;

public interface IKalmanFilter
{
    int NumberOfStates { get; set; }

    int NumberOfUpdates { get; set; }

    double LastUpdateTime { get; set; }

    Vector X { get; set; }

    Matrix P { get; set; }

    Matrix Phi { get; set; }

    Matrix Q { get; set; }

    Vector XPred { get; set; }

    Matrix PPred { get; set; }

    Vector Z { get; set; }

    Matrix R { get; set; }

    Vector ZPred { get; set; }

    Matrix H { get; set; }

    Vector Innovation { get; set; }

    Matrix S { get; set; }

    Vector DeltaX { get; set; }

    Matrix K { get; set; }

    Vector QSD { get; set; }

    void Initialise(double time, Vector x, Matrix p);

    void Update(double time, Vector z, Matrix r, Vector ownshipStates);

    (Vector xPred, Matrix PPred) Predict(double time);

    Matrix CalculateTransitionMatrix(Vector x, double dt);

    Matrix CalculateProcessNoiseMatrix(Vector x, double dt);

    Vector CalculatePredictedMeasurementVector(Vector xPred);

    Matrix CalculatePredictedMeasurementMatrix(Vector xPred);
}