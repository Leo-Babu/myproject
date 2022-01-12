using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUserName(string username);
        User GetUserByUserNameAndPassword(string username, string password);
        bool RegisterUser(User user);
    }
}