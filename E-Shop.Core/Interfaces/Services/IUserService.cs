using E_Shop.Core.Entities;

namespace E_Shop.Core.Interfaces.Services
{
    public interface IUserService
    {
        User GetUser(int id);
        User GetUser(string username);
        bool IsUserExists(string username);
    }
}
