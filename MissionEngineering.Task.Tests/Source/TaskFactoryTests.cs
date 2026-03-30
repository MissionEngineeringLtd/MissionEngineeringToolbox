using MissionEngineering.Core;

namespace MissionEngineering.Task.Tests;

[TestClass]
public sealed class TaskFactoryTests
{
    [TestMethod]
    public void GenerateFakeTasks_ExpectSuccess()
    {
        // Arrange
        var numberOfTasks = 100;

        // Act
        var tasks = TaskFactory.GenerateFakeTasks(numberOfTasks);

        var outputFolder = Environment.CurrentDirectory;

        var outputFile = Path.Combine(outputFolder, "Tasks.csv");

        tasks.WriteToCsvFile(outputFile);

        // Assert
        Assert.AreEqual(tasks.Count, numberOfTasks);
    }
}