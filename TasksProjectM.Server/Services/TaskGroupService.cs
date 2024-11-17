using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class TaskGroupService : ITaskGroupService
    {
        private readonly ITaskGroupRepository _taskGroupRepository;

        public TaskGroupService(ITaskGroupRepository taskGroupRepository)
        {
            _taskGroupRepository = taskGroupRepository;
        }

        public async Task<IEnumerable<Models.TaskGroup>> GetAllAsync()
        {
            return await _taskGroupRepository.GetAllAsync();
        }

        public async Task<Models.TaskGroup> GetByIdAsync(int id)
        {
            return await _taskGroupRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Models.TaskGroup taskGroup)
        {
            await _taskGroupRepository.AddAsync(taskGroup);
        }

        public async Task UpdateAsync(Models.TaskGroup taskGroup)
        {
            await _taskGroupRepository.UpdateAsync(taskGroup);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskGroupRepository.DeleteAsync(id);
        }
    }
}
