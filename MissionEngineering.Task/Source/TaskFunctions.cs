namespace MissionEngineering.Task;

public static class TaskFunctions
{
    public static TaskQualityStatusType GetTaskQualityStatus(double taskQualityMinimum, double taskQualityDesired, double taskQualityAchieved)
    {
        var taskQualityStatus = TaskQualityStatusType.Undefined;

        if (taskQualityAchieved >= taskQualityDesired)
        {
            taskQualityStatus = TaskQualityStatusType.AboveQDes;
        }
        else if (taskQualityAchieved >= taskQualityMinimum)
        {
            taskQualityStatus = TaskQualityStatusType.AboveQMin;
        }
        else
        {
            taskQualityStatus = TaskQualityStatusType.BelowQMin;
        }

        return taskQualityStatus;
    }
}