using AuthService.Exceptions;
using AuthService.Models;
using AuthService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository)
        {
            this._userRepository = repository;
        }
        public bool RegisterUser(User user)
        {
            User obj = _userRepository.GetUserByUserName(user.UserName);
            if (obj == null)
            {
                _userRepository.RegisterUser(user);
                return true;
            }
            else
            {
                throw new UserAlreadyExistException($"This username: {user.UserName} already exists");
            }
        }
        public User GetUserByUserName(string username)
        {
            User obj = _userRepository.GetUserByUserName(username);
            if (obj == null)
            {
                throw new UserNotFoundException($"User with username: {username} does not exist");
            }
            return obj;
        }
        public User GetUserByUserNameAndPassword(string username, string password)
        {
            User obj = _userRepository.GetUserByUserNameAndPassword(username, password);
            if (obj == null)
            {
                throw new UserNotFoundException($"User with username: {username} does not exist");
            }
            return obj;
        }
    }
}
