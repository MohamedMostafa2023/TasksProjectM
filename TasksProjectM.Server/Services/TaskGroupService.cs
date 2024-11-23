using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class TaskGroupService : ITaskGroupService
    {
        private readonly ITaskGroupRepository _taskGroupRepository;

        private readonly ITaskRepository _taskRepository;


        public TaskGroupService(ITaskGroupRepository taskGroupRepository, ITaskRepository taskRepository)
        {
            _taskGroupRepository = taskGroupRepository;
            _taskRepository = taskRepository;
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

        //public async Task DeleteAsync(int id)
        //{
        //    await _taskGroupRepository.DeleteAsync(id);
        //}

        public async Task DeleteAsync(int id)
        {
            // Step 1: Get all tasks associated with the task group
            var tasks = await _taskRepository.GetByTaskGroupIdAsync(id);

            // Step 2: Delete each task associated with the group
            foreach (var task in tasks)
            {
                await _taskRepository.DeleteAsync(task.TaskId);
            }

            // Step 3: Delete the task group itself
            await _taskGroupRepository.DeleteAsync(id);
        }

    }
}
