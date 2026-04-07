using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform.Tests
{
    [TestClass]
    public sealed class PlatformFunctionsTests
    {
        [TestMethod]
        public void GeneratePlatformStateRelative_WithValidStates_ExpectSuccess()
        {
            // Arrange:
            var startDateTime = new DateTime(2024, 1, 1, 12, 0, 0);

            var dateTimeOrigin = new DateTimeOrigin(startDateTime);

            var simulationClock = new SimulationClock(dateTimeOrigin);
  
            var timeStamp = simulationClock.GetTimeStamp(10.0);

            var originPositionNED = new PositionNED { PositionNorth_m = 1000.0, PositionEast_m = 0.0, PositionDown_m = 0.0 };
            var originVelocityNED = new VelocityNED { VelocityNorth_ms = 100.0, VelocityEast_ms = 0.0, VelocityDown_ms = 0.0 };
            var originAttitude = FrameConversions.GetAttitudeFromVelocityVector(originVelocityNED);

            var targetPositionNED = new PositionNED { PositionNorth_m = 10000.0, PositionEast_m = 0.0, PositionDown_m = 0.0 };
            var targetVelocityNED = new VelocityNED { VelocityNorth_ms = 145.0, VelocityEast_ms = -45.0, VelocityDown_ms = 0.0 };
            var targetAttitude = FrameConversions.GetAttitudeFromVelocityVector(targetVelocityNED);

            var platformStateOrigin = new PlatformState
            {
                TimeStamp = timeStamp,
                PlatformId = 1,
                PlatformName = "OriginPlatform",
                PositionNED = originPositionNED,
                VelocityNED = originVelocityNED,
                Attitude = originAttitude
            };

            var platformStateTarget = new PlatformState
            {
                TimeStamp = timeStamp,
                PlatformId = 2,
                PlatformName = "TargetPlatform",
                PositionNED = targetPositionNED,
                VelocityNED = targetVelocityNED,
                Attitude = targetAttitude
            };

            // Act:
            var relativeState = PlatformFunctions.GeneratePlatformStateRelative(platformStateOrigin, platformStateTarget);

            // Assert:
            var expectedResult = -135.0;

            Assert.AreEqual(expectedResult, relativeState.PresentationAngleAzimuth_deg, 1.0e-8);
        }
    }
}
