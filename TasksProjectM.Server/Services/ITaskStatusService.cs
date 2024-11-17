using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public interface ITaskStatusService
    {
        Task<IEnumerable<Models.TaskStatus>> GetAllAsync();
        Task<Models.TaskStatus> GetByIdAsync(int id);
        Task AddAsync(Models.TaskStatus taskStatus);
        Task UpdateAsync(Models.TaskStatus taskStatus);
        Task DeleteAsync(int id);
    }
}
