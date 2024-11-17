using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Task>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async System.Threading.Tasks.Task<Models.Task> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async System.Threading.Tasks.Task AddAsync(Models.Task task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async System.Threading.Tasks.Task UpdateAsync(Models.Task task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Task>> GetTasksByTaskGroupAsync(int taskGroupId)
        {
            return await _taskRepository.GetByTaskGroupIdAsync(taskGroupId);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Task>> GetByTaskNameAsync(string taskName)
        {
            return await _taskRepository.GetByTaskNameAsync(taskName);
        }
    }
}
