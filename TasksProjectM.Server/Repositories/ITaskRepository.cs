namespace TasksProjectM.Server.Repositories
{
    public interface ITaskRepository : IGenericRepository<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetByTaskGroupIdAsync(int taskGroupId);

        Task<IEnumerable<Models.Task>> GetByTaskNameAsync(string taskName);
    }
}
