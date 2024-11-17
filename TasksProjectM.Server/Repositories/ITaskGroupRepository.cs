using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public interface ITaskGroupRepository : IGenericRepository<TaskGroup>
    {
        // Add any TaskGroup-specific methods here if needed
        Task<IEnumerable<TaskGroup>> GetTaskGroupsByNameAsync(string name);
    }
}
