using E_Shop.Core.Entities;
using System.Collections.Generic;

namespace E_Shop.Core.Interfaces.Services
{
    public interface IUserCartService
    {
        IEnumerable<UserCart> GetUserCart(int userId);
        void Save();
    }
}
