using E_Shop.Core.Entities;
using System;
using System.Collections.Generic;

namespace E_Shop.Core.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(Func<Product, bool> predicate);
        Product GetProduct(int id);
    }
}
