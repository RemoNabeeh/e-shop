using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace E_Shop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _users;

        public UserRepository()
        {
            var xmlManager = new XmlManager(Constants.UsersFilePath);
            _users = xmlManager.Read<User>();
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(e => e.Id == id);
        }

        public User GetUser(string username)
        {
            return _users.FirstOrDefault(e => e.Username.ToLower() == username.ToLower());
        }
    }
}
