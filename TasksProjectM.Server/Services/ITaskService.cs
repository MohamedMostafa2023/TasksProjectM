namespace TasksProjectM.Server.Services
{
    public interface ITaskService : IGenericService<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetTasksByTaskGroupAsync(int taskGroupId);

        Task<IEnumerable<Models.Task>> GetByTaskNameAsync(string taskName);
    }
}
