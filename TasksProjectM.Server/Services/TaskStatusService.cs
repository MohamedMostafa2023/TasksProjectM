using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskStatusService(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        public async Task<IEnumerable<Models.TaskStatus>> GetAllAsync()
        {
            return await _taskStatusRepository.GetAllAsync();
        }

        public async Task<Models.TaskStatus> GetByIdAsync(int id)
        {
            return await _taskStatusRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Models.TaskStatus taskStatus)
        {
            await _taskStatusRepository.AddAsync(taskStatus);
        }

        public async Task UpdateAsync(Models.TaskStatus taskStatus)
        {
            await _taskStatusRepository.UpdateAsync(taskStatus);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskStatusRepository.DeleteAsync(id);
        }
    }
}
