using AuthService.Models;

namespace AuthService.Services
{
    public interface IUserService
    {
        User GetUserByUserName(string username);
        User GetUserByUserNameAndPassword(string username, string password);
        bool RegisterUser(User user);
    }
}