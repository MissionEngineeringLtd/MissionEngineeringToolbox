using Bogus;

namespace MissionEngineering.Task;

public static class TaskFactory
{
    public static ITask GenerateFakeTask()
    {
        var faker = new Faker("en");

        var task = GenerateFakeTask(faker);

        return task;
    }

    public static List<ITask> GenerateFakeTasks(int numberOfTasks)
    {
        var faker = new Faker("en");

        var taskList = new List<ITask>(numberOfTasks);

        for (int i = 0; i < numberOfTasks; i++)
        {
            var task = GenerateFakeTask(faker);

            var taskId = i + 1;

            task.TaskHeader.TaskId = taskId;
            task.TaskHeader.TaskIdParent = (i / 10) + 1;

            if (i == 0)
            {
                task.TaskHeader.TaskIdParent = -1;
            }

            taskList.Add(task);
        }

        return taskList;
    }

    private static ITask GenerateFakeTask(Faker faker)
    {
        var taskHeader = new TaskHeader()
        {
            TaskIdParent = faker.Random.Number(1, 5),
            TaskId = faker.Random.Number(10, 100),
            TaskName = faker.Random.Words(3),
            TaskDescription = faker.Random.Words(10),
            TaskLevel = faker.Random.Enum<TaskLevelType>(),
            TaskType = faker.Random.Enum<TaskType>(),
            TaskDemandType = faker.Random.Enum<TaskDemandType>(),
            TaskCreationDate = faker.Date.Past(),
            TaskModificationDate = faker.Date.Past(),
            TaskModificationCount = faker.Random.Number(1, 10),
            TaskQualityMinimum = faker.Random.Number(100),
            TaskQualityDesired = faker.Random.Number(100),
            TaskStatusType = faker.Random.Enum<TaskStatusType>(),
            TaskQualityAchieved = faker.Random.Number(100),
        };

        var task = new Task_Mission_Search()
        {
            TaskHeader = taskHeader
        };

        return task;
    }
}