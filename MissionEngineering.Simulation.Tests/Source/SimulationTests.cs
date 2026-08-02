namespace MissionEngineering.Simulation;

[TestClass]
public sealed class SimulationTests
{
    [TestMethod]
    public void Run_WithValidData_ExpectSuccess()
    {
        // Arrange
        var simulationSettings = SimulationSettingsFactory.SimulationSettings_Single("Simulation_1", "");

        simulationSettings.IsAddConsoleLogging = false;
        simulationSettings.IsAddFileLogging = false;
        simulationSettings.IsWriteData = false;
        simulationSettings.IsCreateZipFile = false;

        var scenarioSettings = ScenarioSettingsFactory.ScenarioSettings_Test_1();

        var simulationEvents = SimulationEventFactory.FF_1();

        var simulationHarness = SimulationBuilder.CreateSimulationHarness();

        simulationHarness.SimulationSettings = simulationSettings;
        simulationHarness.ScenarioSettings = scenarioSettings;
        simulationHarness.SimulationEvents = simulationEvents;
        simulationHarness.SimulationHarnessSettings.NumberOfRuns = 1;

        // Act
        simulationHarness.Run();

        // Assert
        var expectedNumberOfModels = 1;

        Assert.HasCount(expectedNumberOfModels, simulationHarness.Simulation.SimulationModels);
    }
}