namespace MissionEngineering.Task;

public enum TaskStatusType
{
    Undefined = 0,
    NotStarted,
    Rejected,
    InProgress,
    Suspended,
    Completed,
    Cancelled,
    TimedOut,
    Failed
}