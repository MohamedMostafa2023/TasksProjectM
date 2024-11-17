using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public class TaskStatusRepository : GenericRepository<Models.TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(AppDbContext context) : base(context) { }

        // Example of custom method if needed
        public async Task<IEnumerable<Models.TaskStatus>> GetTaskStatusesByColorAsync(string color)
        {
            return await _context.TaskStatus
                .Where(ts => ts.StatusColor == color)
                .ToListAsync();
        }
    }
}
