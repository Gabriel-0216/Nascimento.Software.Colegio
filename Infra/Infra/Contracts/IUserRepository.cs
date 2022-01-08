using Domain.Users;

namespace Infra.Infra.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string Email);
        Task<User> GetUserByPhone(string phone);
        Task<bool> Register(User user);
        Task<User> Login(User user);
        Task<bool> DeleteUser(User user);
    }
}
