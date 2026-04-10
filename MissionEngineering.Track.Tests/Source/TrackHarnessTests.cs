using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Track.Tests
{
    [TestClass]
    public sealed class TrackHarnessTests
    {
        [TestMethod]
        public void Run_WithValidData_ExpectSuccess()
        {
            // Arrange
            var dateTimeStart = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc);

            var dateTimeOrigin = new DateTimeOrigin(dateTimeStart);

            var simulationClock = new SimulationClock(dateTimeOrigin);

            var llaOrigin = new LLAOrigin
            {
                PositionLLA = new PositionLLA(51.5074, -0.1278, 0)
            };

            // Sensor:
            var positionNEDSensor = new PositionNED(0, 0, 0);
            var velocityNEDSensor = new VelocityNED(0, 0, 0);
            var positionLLASensor = MappingConversions.ConvertPositionNEDToPositionLLA(positionNEDSensor, llaOrigin.PositionLLA);
            var attitudeSensor = FrameConversions.GetAttitudeFromVelocityVector(velocityNEDSensor);

            // Target:
            var positionNEDTarget = new PositionNED(10000, 0, -2000);
            var velocityNEDTarget = new VelocityNED(-200, 0, 0);
            var positionLLATarget = MappingConversions.ConvertPositionNEDToPositionLLA(positionNEDTarget, llaOrigin.PositionLLA);
            var attitudeTarget = FrameConversions.GetAttitudeFromVelocityVector(velocityNEDTarget);

            var timeStamp = simulationClock.GetTimeStamp(0.0);

            var platformStateSensor = new PlatformState
            {
                TimeStamp = timeStamp,
                PlatformId = 1,
                PlatformName = "Sensor Platform",
                PositionLLA = positionLLASensor,
                PositionNED = positionNEDSensor,
                VelocityNED = velocityNEDSensor,
                AccelerationNED = new AccelerationNED(0, 0, 0),
                Attitude = attitudeSensor,
                AttitudeRate = new AttitudeRate(0, 0, 0)
            };

            var platformStateTarget = new PlatformState
            {
                TimeStamp = timeStamp,
                PlatformId = 2,
                PlatformName = "Target Platform",
                PositionLLA = positionLLATarget,
                PositionNED = positionNEDTarget,
                VelocityNED = velocityNEDTarget,
                AccelerationNED = new AccelerationNED(0, 0, 0),
                Attitude = attitudeTarget,
                AttitudeRate = new AttitudeRate(0, 0, 0)
            };

            var trackHarness = new TrackHarness(simulationClock, llaOrigin)
            {
                StartTime = 0.0,
                EndTime = 20.0,
                UpdateTimeStep = 2.0,
                PredictionTimeStep = 0.1,
                PlatformStateSensor = platformStateSensor,
                PlatformStateTarget = platformStateTarget,
            };

            // Act
            trackHarness.Run();

            // Analyse
            var fileName = Path.Combine(Environment.CurrentDirectory, "TrackHarnessTestOutput.csv");

            trackHarness.TrackDataPredictedList.WriteToCsvFile(fileName);

            // Assert
            Assert.AreEqual(trackHarness.PredictionTimes.NumberOfElements, trackHarness.TrackDataPredictedList.Count);
        }
    }
}