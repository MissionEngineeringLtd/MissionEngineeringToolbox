using MissionEngineering.Core;
using MissionEngineering.Platform;
using MissionEngineering.Math;

namespace MissionEngineering.Scanner.Tests
{
    [TestClass]
    public sealed class ScannerHarnessTests
    {
        [TestMethod]
        public void Run_Test_1()
        {
            // Arrange:
            var dateTimeStart = new DateTime(2024, 1, 1, 12, 0, 0);
            var dateTimeOrigin = new DateTimeOrigin(dateTimeStart);
            var simulationClock = new SimulationClock(dateTimeOrigin);

            var platformState = new PlatformState()
            {
                PlatformId = 1,
                PlatformName = "TestPlatform",
                Attitude = new Attitude(80.0, 0.0, 0.0)
            };

            var scanSettings = new ScanSettings()
            {
                PlatformId = platformState.PlatformId,
                PlatformName = platformState.PlatformName,
                ScannerId = 1,
                ScannerName = "TestScanner",
                ScanType = ScanType.CircularScan,
                InitialScanAzimuthAngle_Body_deg = 30.0,
                InitialScanElevationAngle_Body_deg = 1.0,
                MaximumScanAzimuthRate_RPM = 48.0,
            };

            var scannerHarness = new ScannerHarness()
            {
                SimulationClock = simulationClock,
                ScanSettings = scanSettings,
                PlatformState = platformState,
                StartTime_s = 10.0,
                EndTime_s = 100.0,
                TimeStep_s = 0.1
            };

            // Act:
            scannerHarness.Run();

            // Assert:
            var expectedScanNumber = 73;
            var scanNumber = scannerHarness.Scanner.ScanData.ScanNumber;

            Assert.AreEqual(expectedScanNumber, scanNumber);
        }
    }
}
