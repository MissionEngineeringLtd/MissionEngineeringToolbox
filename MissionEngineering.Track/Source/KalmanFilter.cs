using MissionEngineering.Math;

namespace MissionEngineering.Track;

public abstract class KalmanFilter : IKalmanFilter
{
    public int NumberOfStates { get; set; }

    public int NumberOfUpdates { get; set; }

    public double LastUpdateTime { get; set; }

    public Vector X { get; set; }

    public Matrix P { get; set; }

    public Matrix Phi { get; set; }

    public Matrix Q { get; set; }

    public Vector XPred { get; set; }

    public Matrix PPred { get; set; }

    public Vector Z { get; set; }

    public Matrix R { get; set; }

    public Vector ZPred { get; set; }

    public Matrix H { get; set; }

    public Vector Innovation { get; set; }

    public Matrix S { get; set; }

    public Vector DeltaX { get; set; }

    public Matrix K { get; set; }

    public Vector QSD { get; set; }

    public virtual void Initialise(double time, Vector x, Matrix p)
    {
        X = x;
        P = p;

        NumberOfUpdates = 1;

        LastUpdateTime = time;
    }

    public virtual void Update(double time, Vector z, Matrix r, Vector ownshipStates)
    {
        var dt = time - LastUpdateTime;

        Phi = CalculateTransitionMatrix(X, dt);
        Q = CalculateProcessNoiseMatrix(X, dt);

        XPred = Phi * X;
        PPred = Phi * P * Phi.Transpose() + Q;

        XPred = XPred - ownshipStates;

        Z = z;
        R = r;

        ZPred = CalculatePredictedMeasurementVector(XPred);
        H = CalculatePredictedMeasurementMatrix(XPred);

        Innovation = z - ZPred;
        S = H * PPred * H.Transpose() + R;

        K = PPred * H.Transpose() * S.Inverse();

        DeltaX = K * Innovation;

        X = XPred + DeltaX;
        P = PPred - K * H * PPred;

        X = X + ownshipStates;

        NumberOfUpdates++;

        LastUpdateTime = time;
    }

    public virtual (Vector xPred, Matrix PPred) Predict(double time)
    {
        var dt = time - LastUpdateTime;

        var phi = CalculateTransitionMatrix(X, dt);
        var q = CalculateProcessNoiseMatrix(X, dt);

        var xPred = phi * X;
        var pPred = phi * P * phi.Transpose() + q;

        return (xPred, pPred);
    }

    public abstract Matrix CalculateTransitionMatrix(Vector x, double dt);

    public abstract Matrix CalculateProcessNoiseMatrix(Vector x, double dt);

    public abstract Vector CalculatePredictedMeasurementVector(Vector xPred);

    public abstract Matrix CalculatePredictedMeasurementMatrix(Vector xPred);
}