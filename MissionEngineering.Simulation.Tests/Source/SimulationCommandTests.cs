using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

[TestClass]
public sealed class SimulationCommandTests
{
    [TestMethod]
    public void WriteCommandsToYamlFile_ExpectSuccess()
    {
        // Arrange
        var commands = SimulationCommandExamples.Example_1();

        var yamlString = commands.ConvertToYamlString();

        var outputFolder = Environment.CurrentDirectory;

        var outputFile = Path.Combine(outputFolder, "SimulationCommmands_Example_1.yaml");

        // Act
        commands.WriteToYamlFile(outputFile);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(yamlString));
    }

    [TestMethod]
    public void ReadCommandsFromYamlFile_ExpectSuccess()
    {
        // Arrange
        var commandsIn = SimulationCommandExamples.Example_1();

        var yamlString = commandsIn.ConvertToYamlString();

        var outputFolder = Environment.CurrentDirectory;

        var outputFile = Path.Combine(outputFolder, "SimulationCommmands_Example_2.yaml");

        commandsIn.WriteToYamlFile(outputFile);

        var inputFolder = Environment.CurrentDirectory;

        var inputFile = Path.Combine(inputFolder, "SimulationCommmands_Example_2.yaml");

        // Act
        var commandsOut = SimulationCommandManager.ReadCommandsFromFile(inputFile);

        // Assert
        var expectedCommandType = SimulationCommandType.PlatformCreate;

        var actualCommandType = commandsOut[2].CommandType;

        Assert.AreEqual(expectedCommandType, actualCommandType);
        Assert.AreEqual(4, commandsOut.Count);
    }
}