using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public interface ITaskStatusRepository : IGenericRepository<Models.TaskStatus>
    {
        // Add any TaskStatus-specific methods here if needed
        Task<IEnumerable<Models.TaskStatus>> GetTaskStatusesByColorAsync(string color);
    }
}
