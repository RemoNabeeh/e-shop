using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Infrastructure.Helpers;
using System;
using System.Collections.Generic;

namespace E_Shop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAll(Func<Product, bool> predicate)
        {
            
            var xmlManager = new XmlManager(Constants.ProductsFilePath);
            return xmlManager.Read<Product>();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
