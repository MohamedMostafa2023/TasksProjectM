using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async System.Threading.Tasks.Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async System.Threading.Tasks.Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName);
        }
    }
}
