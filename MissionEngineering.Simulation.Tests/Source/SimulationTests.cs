namespace MissionEngineering.Simulation.Tests
{
    [TestClass]
    public sealed class SimulationTests
    {
        [TestMethod]
        public void Run_WithValidData_ExpectSuccess()
        {
            // Arrange
            var simulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Single();
            var scenarioSettings = ScenarioSettingsFactory.ScenarioSettings_Test_1();

            var simulationHarness = SimulationBuilder.CreateSimulationHarness();

            simulationHarness.SimulationSettings = simulationSettings;
            simulationHarness.ScenarioSettings = scenarioSettings;
            simulationHarness.SimulationHarnessSettings.NumberOfRuns = 1;

            // Act
            simulationHarness.Run();

            // Assert
            Assert.AreEqual(simulationHarness.Simulation.SimulationModels.Count, 2);
        }
    }
}