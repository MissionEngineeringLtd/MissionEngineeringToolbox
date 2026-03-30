namespace MissionEngineering.Task;

public record TaskHeader
{
    public int TaskIdParent { get; set; }

    public int TaskId { get; set; }

    public string TaskName { get; set; } = "";

    public string TaskDescription { get; set; } = "";

    public TaskLevelType TaskLevel { get; set; }

    public TaskType TaskType { get; set; }

    public TaskDemandType TaskDemandType { get; set; }

    public DateTime TaskCreationDate { get; set; }

    public DateTime TaskModificationDate { get; set; }

    public int TaskModificationCount { get; set; }

    public double TaskQualityMinimum { get; set; }

    public double TaskQualityDesired { get; set; }

    public TaskStatusType TaskStatusType { get; set; }

    public double TaskQualityAchieved { get; set; }

    public TaskQualityStatusType TaskQualityStatus => TaskFunctions.GetTaskQualityStatus(TaskQualityMinimum, TaskQualityDesired, TaskQualityAchieved);
}