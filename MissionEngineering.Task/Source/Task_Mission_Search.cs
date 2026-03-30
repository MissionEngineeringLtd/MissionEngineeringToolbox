namespace MissionEngineering.Task;

public class Task_Mission_Search : Task
{
    public Task_Mission_Search()
    {
        TaskHeader = new TaskHeader
        {
            TaskId = 0,
            TaskName = "",
            TaskDescription = "",
            TaskType = TaskType.Mission_Search,
            TaskDemandType = TaskDemandType.Create,
            TaskLevel = TaskLevelType.Mission,
            TaskCreationDate = DateTime.UtcNow,
            TaskModificationDate = DateTime.UtcNow,
            TaskModificationCount = 0
        };
    }
}