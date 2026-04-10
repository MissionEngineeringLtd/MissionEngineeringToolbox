using MissionEngineering.Math;

using static System.Math;

namespace MissionEngineering.Track;

public class KalmanFilterAirTrack_9State_ConstantTurnRate : KalmanFilter
{
    public double Omega { get; set; }

    public KalmanFilterAirTrack_9State_ConstantTurnRate()
    {
        NumberOfStates = 9;

        QSD = new Vector([10.0, 10.0, 10.0]);

        Omega = 0.001;
    }

    public override void Update(double time, Vector z, Matrix r, Vector ownshipStates)
    {
        base.Update(time, z, r, ownshipStates);

        CalculateTurnRate();
    }

    public override Matrix CalculateTransitionMatrix(Vector x, double dt)
    {
        var phi = Matrix.IdentityMatrix(NumberOfStates, NumberOfStates);

        var halfdtSquared = 0.5 * dt * dt;

        phi[0, 3] = dt;
        phi[1, 4] = dt;
        phi[2, 5] = dt;

        phi[0, 6] = halfdtSquared;
        phi[1, 7] = halfdtSquared;
        phi[2, 8] = halfdtSquared;

        phi[3, 6] = dt;
        phi[4, 7] = dt;
        phi[5, 8] = dt;

        return phi;
    }

    public override Matrix CalculateProcessNoiseMatrix(Vector x, double dt)
    {
        var q = new Matrix(NumberOfStates, NumberOfStates);

        var qSub = new Matrix(3, 3);

        qSub[0, 0] = Pow(dt, 5) / 20.0;
        qSub[0, 1] = Pow(dt, 4) / 8.0;
        qSub[0, 2] = Pow(dt, 3) / 6.0;
        qSub[1, 0] = qSub[0, 1];
        qSub[1, 1] = Pow(dt, 3) / 3.0;
        qSub[1, 2] = Pow(dt, 2) / 2.0;
        qSub[2, 0] = qSub[0, 2];
        qSub[2, 1] = qSub[1, 2];
        qSub[2, 2] = dt;

        q[[0, 3, 6], [0, 3, 6]] = qSub * QSD[0] * QSD[0];
        q[[1, 4, 7], [1, 4, 7]] = qSub * QSD[1] * QSD[1];
        q[[2, 5, 8], [2, 5, 8]] = qSub * QSD[2] * QSD[2];

        return q;
    }

    public override Matrix CalculatePredictedMeasurementMatrix(Vector xPred)
    {
        var h = Matrix.IdentityMatrix(6, NumberOfStates);

        return h;
    }

    public override Vector CalculatePredictedMeasurementVector(Vector xPred)
    {
        var zPred = new Vector(xPred[0..6]);

        return zPred;
    }

    public void CalculateTurnRate()
    {
        var velocity = new Vector(X[3..5]);
        var acceleration = new Vector(X[6..8]);

        var turnRate = acceleration.Norm() / velocity.Norm();

        Omega = turnRate;

        Omega = Max(Omega, 0.001);
    }
}