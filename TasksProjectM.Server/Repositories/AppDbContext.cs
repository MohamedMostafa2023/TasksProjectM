using Microsoft.EntityFrameworkCore;
using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.TaskStatus> TaskStatus { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
