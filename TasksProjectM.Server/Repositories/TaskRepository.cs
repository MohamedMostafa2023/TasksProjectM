using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public class TaskRepository : GenericRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        // Custom method to get tasks by TaskGroupId
        public async Task<IEnumerable<Models.Task>> GetByTaskGroupIdAsync(int taskGroupId)
        {
            return await _context.Tasks
                .Where(t => t.TaskGroupId == taskGroupId)
                .ToListAsync();
        }

        // Custom method to get tasks by TaskName
        public async Task<IEnumerable<Models.Task>> GetByTaskNameAsync(string taskName)
        {
            return await _context.Tasks
                .Where(t => t.TaskName == taskName)
                .ToListAsync();
        }
    }
}
