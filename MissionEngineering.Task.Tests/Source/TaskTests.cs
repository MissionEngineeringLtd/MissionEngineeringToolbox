namespace MissionEngineering.Task.Tests;

[TestClass]
public sealed class TaskTests
{
    [TestMethod]
    public void CreateTask_WithTaskDemand_ExpectSuccess()
    {
        // Arrange
        var taskHeader = new TaskHeader
        {
            TaskId = 1001,
            TaskName = "Mission Task 1",
            TaskDescription = "Mission Task 1 Description",
            TaskLevel = TaskLevelType.Mission,
            TaskType = TaskType.Mission_Search,
            TaskDemandType = TaskDemandType.Create,
            TaskCreationDate = DateTime.UtcNow,
            TaskModificationDate = DateTime.UtcNow,
            TaskModificationCount = 1,
            TaskQualityMinimum = 10,
            TaskQualityDesired = 20,
            TaskStatusType = TaskStatusType.InProgress,
            TaskQualityAchieved = 18,
        };

        // Act
        var task = new Task_Mission_Search
        {
            TaskHeader = taskHeader
        };

        // Assert
        Assert.AreEqual(task.TaskHeader.TaskQualityStatus, TaskQualityStatusType.AboveQMin);
    }
}