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

        var outputFolder = @"C:\Temp\MissionEngineeringToolbox";

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
        var inputFolder = @"C:\Temp\MissionEngineeringToolbox";

        var inputFile = Path.Combine(inputFolder, "SimulationCommmands_Example_1.yaml");

        // Act
        var commands = SimulationCommandHelper.ReadCommandsFromFile(inputFile);

        // Assert
        Assert.AreEqual(2, commands.Count);
    }
}