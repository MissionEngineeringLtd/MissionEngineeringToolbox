namespace MissionEngineering.Antenna.Tests
{
    [TestClass]
    public sealed class AntennaModelTests
    {
        [TestMethod]
        public void GenerateAntennaPattern_WithValidSettings_ExpectSuccess()
        {
            // Act:
            var antennaModelSettings = AntennaModelSettingsExamples.Example_1();

            var antennaModel = new AntennaModel
            {
                AntennaModelSettings = antennaModelSettings
            };

            var expectedNumberOfAzimuthAngles = 361;

            // Arrange:
            antennaModel.GenerateAntenna();

            // Assert:
            Assert.AreEqual(expectedNumberOfAzimuthAngles, antennaModel.NumberOfAzimuthAngles);
        }
    }
}
