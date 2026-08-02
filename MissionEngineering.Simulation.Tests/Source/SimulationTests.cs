namespace MissionEngineering.Simulation;

[TestClass]
public sealed class SimulationTests
{
    [TestMethod]
    public void Run_WithValidData_ExpectSuccess()
    {
        // Arrange
        var simulationRunSettings = SimulationRunSettingsFactory.SingleRun("Simulation_1", "");

        simulationRunSettings.IsAddConsoleLogging = false;
        simulationRunSettings.IsAddFileLogging = false;
        simulationRunSettings.IsWriteData = false;
        simulationRunSettings.IsCreateZipFile = false;

        var simulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1();

        var simulationEvents = SimulationEventFactory.FF_1();

        var simulationHarness = SimulationBuilder.CreateSimulationHarness();

        simulationHarness.SimulationRunSettings = simulationRunSettings;
        simulationHarness.SimulationSettings = simulationSettings;
        simulationHarness.SimulationEvents = simulationEvents;
        simulationHarness.SimulationHarnessSettings.NumberOfRuns = 1;

        // Act
        simulationHarness.Run();

        // Assert
        var expectedNumberOfModels = 1;

        Assert.HasCount(expectedNumberOfModels, simulationHarness.Simulation.SimulationModels);
    }
}