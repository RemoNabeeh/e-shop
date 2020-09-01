using E_Shop.Core.Entities;

namespace E_Shop.Core.Interfaces.Repositories
{
    public interface IUserCartRepository
    {
        UserCart GetUserCart(int userId);
    }
}
