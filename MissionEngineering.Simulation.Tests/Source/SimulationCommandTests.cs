using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

[TestClass]
public sealed class SimulationEventTests
{
    [TestMethod]
    public void WriteEventsToYamlFile_ExpectSuccess()
    {
        // Arrange
        var events = SimulationEventFactory.Example_1();

        var yamlString = events.ConvertToYamlString();

        var outputFolder = Environment.CurrentDirectory;

        var outputFile = Path.Combine(outputFolder, "SimulationEvents_Example_1.yaml");

        // Act
        events.WriteToYamlFile(outputFile);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(yamlString));
    }

    [TestMethod]
    public void ReadEventsFromYamlFile_ExpectSuccess()
    {
        // Arrange
        var eventsIn = SimulationEventFactory.Example_1();

        var yamlString = eventsIn.ConvertToYamlString();

        var outputFolder = Environment.CurrentDirectory;

        var outputFile = Path.Combine(outputFolder, "SimulationEvents_Example_2.yaml");

        eventsIn.WriteToYamlFile(outputFile);

        var inputFolder = Environment.CurrentDirectory;

        var inputFile = Path.Combine(inputFolder, "SimulationEvents_Example_2.yaml");

        // Act
        var eventsOut = SimulationEventManager.ReadEventsFromFile(inputFile);

        // Assert
        var expectedEventType = SimulationEventType.PlatformCreate;

        var actualEventType = eventsOut[0].EventType;

        Assert.AreEqual(expectedEventType, actualEventType);
        Assert.AreEqual(11, eventsOut.Count);
    }
}