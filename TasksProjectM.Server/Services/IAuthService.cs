using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserForRegistration userForRegistration);
        Task<string> LoginAsync(UserForLogin userForLogin);
    }
}
