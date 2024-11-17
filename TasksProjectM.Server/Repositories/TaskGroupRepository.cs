using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public class TaskGroupRepository : GenericRepository<TaskGroup>, ITaskGroupRepository
    {
        public TaskGroupRepository(AppDbContext context) : base(context) { }

        // Example of custom method if needed
        public async Task<IEnumerable<TaskGroup>> GetTaskGroupsByNameAsync(string name)
        {
            return await _context.TaskGroups
                .Where(tg => tg.TaskGroupName.Contains(name))
                .ToListAsync();
        }
    }
}
