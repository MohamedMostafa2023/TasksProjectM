using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public interface ITaskGroupService
    {
        Task<IEnumerable<Models.TaskGroup>> GetAllAsync();
        Task<Models.TaskGroup> GetByIdAsync(int id);
        Task AddAsync(Models.TaskGroup taskGroup);
        Task UpdateAsync(Models.TaskGroup taskGroup);
        Task DeleteAsync(int id);
    }
}
