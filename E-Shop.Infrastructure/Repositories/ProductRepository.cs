using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_Shop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IEnumerable<Product> _products;

        public ProductRepository()
        {
            var xmlManager = new XmlManager(Constants.ProductsFilePath);
            _products = xmlManager.Read<Product>();
        }

        public IEnumerable<Product> GetAll(Func<Product, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public Product GetProduct(int id)
        {
            return _products.FirstOrDefault(e => e.Id == id);
        }
    }
}
