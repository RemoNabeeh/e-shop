using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;

namespace E_Shop.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public bool IsUserExists(string username)
        {
            return _userRepository.GetUser(username) != null;
        }
    }
}
