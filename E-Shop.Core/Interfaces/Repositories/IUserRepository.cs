using E_Shop.Core.Entities;

namespace E_Shop.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string username);
    }
}