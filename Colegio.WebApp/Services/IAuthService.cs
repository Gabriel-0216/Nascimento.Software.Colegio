using Colegio.WebApp.Models;
using Colegio.WebApp.Models.User;
using Colegio.WebApp.Services.LoginResult;

namespace Colegio.WebApp.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Login(LoginViewModel login);
        Task<AuthResult> Register(RegisterViewModel register);
        Task<UserViewModel> GetUserByEmail(string email, string token);


    }
}
