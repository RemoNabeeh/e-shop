using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace E_Shop.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public IEnumerable<Product> GetAll(Func<Product, bool> predicate)
        {
            return _productRepository.GetAll(predicate);
        }

        public Product GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}
