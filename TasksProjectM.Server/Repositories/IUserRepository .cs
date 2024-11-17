using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> RegisterAsync(User user);
        Task<User> LoginAsync(string userName, string password);
        Task<User> GetByUserNameAsync(string userName);
    }
}
