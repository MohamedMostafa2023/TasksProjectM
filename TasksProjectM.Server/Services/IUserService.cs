using TasksProjectM.Server.Models;

namespace TasksProjectM.Server.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
