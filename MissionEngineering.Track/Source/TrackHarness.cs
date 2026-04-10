using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Sensor;

namespace MissionEngineering.Track;

public class TrackHarness : IExecutableModel
{
    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformState PlatformStateSensor { get; set; }

    public PlatformState PlatformStateTarget { get; set; }

    public double StartTime { get; set; }

    public double EndTime { get; set; }

    public double UpdateTimeStep { get; set; }

    public double PredictionTimeStep { get; set; }

    public Vector UpdateTimes { get; set; }

    public Vector PredictionTimes { get; set; }

    public List<TrackDataSmoothed> TrackDataSmoothedList { get; set; }

    public List<TrackDataPredicted> TrackDataPredictedList { get; set; }

    public SensorReport SensorReport { get; set; }

    public Track Track { get; set; }

    public int SensorReportId { get; set; }

    public TrackHarness(ISimulationClock simulationClock, ILLAOrigin llaOrigin)
    {
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
    }

    public void Run()
    {
        UpdateTimes = Vector.LinearlySpacedVector(StartTime, EndTime, UpdateTimeStep);
        PredictionTimes = Vector.LinearlySpacedVector(StartTime, EndTime, PredictionTimeStep);

        var numberOfUpdateSteps = UpdateTimes.NumberOfElements;
        var numberOfPredictSteps = PredictionTimes.NumberOfElements;

        TrackDataSmoothedList = new List<TrackDataSmoothed>(numberOfUpdateSteps);
        TrackDataPredictedList = new List<TrackDataPredicted>(numberOfPredictSteps);

        var numberOfPredictionStepsPerUpdateStep = (int)(UpdateTimeStep / PredictionTimeStep);

        var predictionCount = 0;

        Initialise(StartTime);

        foreach (double time in PredictionTimes)
        {
            var isUpdate = predictionCount == 0;

            if (isUpdate)
            {
                SensorReport = GenerateSensorReport(time);

                Update(time);

                TrackDataSmoothedList.Add(Track.TrackDataSmoothed);
            }

            Predict(time);

            TrackDataPredictedList.Add(Track.TrackDataPredicted);
            predictionCount++;

            if (predictionCount == numberOfPredictionStepsPerUpdateStep)
            {
                predictionCount = 0;
            }
        }
    }

    public void Initialise(double time)
    {
        SensorReportId = 0;
    }

    public void Update(double time)
    {
        SensorReport = GenerateSensorReport(time);

        if (Track is null)
        {
            InitialiseTrack(SensorReport);
        }
        else
        {
            UpdateTrack(SensorReport);
        }
    }

    public void Predict(double time)
    {
        Track.PredictTrack(time);
    }

    public void Finalise(double time)
    {
    }

    public void InitialiseTrack(SensorReport sensorReport)
    {
        Track = new Track(LLAOrigin);

        Track.TrackDataSmoothed.TrackId = 1001;

        Track.InitialiseTrack(SensorReport);
    }

    public void UpdateTrack(SensorReport sensorReport)
    {
        Track.UpdateTrack(SensorReport);
    }

    public SensorReport GenerateSensorReport(double time_s)
    {
        var accelerationTBA = new AccelerationTBA(0.0, 0.0, 0.0);

        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var platformStateSensor = PlatformFunctions.PredictPlatformState(timeStamp, PlatformStateSensor, LLAOrigin.PositionLLA, accelerationTBA, true);
        var platformStateTarget = PlatformFunctions.PredictPlatformState(timeStamp, PlatformStateTarget, LLAOrigin.PositionLLA, accelerationTBA, true);

        var relativeTargetState = PlatformFunctions.GeneratePlatformStateRelative(platformStateSensor, platformStateTarget);

        var polars = relativeTargetState.RelativePolarsNED;

        SensorReportId++;

        var sensorReport = new SensorReport
        {
            SensorPlatformState = platformStateSensor,
            SensorReportHeader = new SensorReportHeader
            {
                TimeStamp = timeStamp,
                SensorId = 2001,
                SensorPlatformId = PlatformStateSensor.PlatformId,
                TargetPlatformId = PlatformStateTarget.PlatformId,
                SensorType = SensorType.Radar,
                SensorReportId = SensorReportId
            },
            TargetLocation = new SensorTargetLocation
            {
                TargetLocationType = SensorTargetLocationType.Geolocation,
                IsPositionLLAValid = true,
                IsPositionNEDValid = true,
                IsVelocityNEDValid = true,
                IsRangeValid = true,
                IsRangeRateValid = true,
                IsAzimuthValid = true,
                IsElevationValid = true,
                IsAltitudeValid = true,
                PositionLLA = platformStateTarget.PositionLLA,
                PositionNED = platformStateTarget.PositionNED,
                VelocityNED = platformStateTarget.VelocityNED,
                Range_m = polars.Range_m,
                RangeRate_ms = polars.RangeRate_ms,
                AzimuthAngle_deg = polars.AzimuthAngle_deg,
                ElevationAngle_deg = polars.ElevationAngle_deg,
            },
            TargetIdentification = new SensorTargetIdentification
            {
                IsPositiveId = true,
                PlatformAffiliation = PlatformAffiliationType.Hostile,
                PlatformClass = "Class A",
                PlatformName = "Target 1",
                PlatformType = PlatformType.Aircraft
            }
        };

        return sensorReport;
    }
}