namespace MissionEngineering.Task;

public abstract class Task : ITask
{
    public TaskHeader TaskHeader { get; set; }
}