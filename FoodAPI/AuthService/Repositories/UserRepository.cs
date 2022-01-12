using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext dbContext)
        {
            this._userContext = dbContext;
        }
        public bool RegisterUser(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return true;
        }
        public User GetUserByUserName(string username)
        {
            User obj = this._userContext.Users
                            .Where(item => item.UserName == username).FirstOrDefault();
            return obj;
        }
        public User GetUserByUserNameAndPassword(string username, string password)
        {
            User obj = this._userContext.Users
                            .Where(item => item.UserName == username && item.Password == password).FirstOrDefault();
            return obj;
        }
    }
}

