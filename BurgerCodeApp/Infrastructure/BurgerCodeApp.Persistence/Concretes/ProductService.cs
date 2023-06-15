using BurgerCodeApp.Application.Interfaces.Repository.ProductRepository;
using BurgerCodeApp.Application.Interfaces.Repository.Services;
using BurgerCodeApp.Domain.Entities;
using BurgerCodeApp.Persistence.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productRead;
        private readonly IProductWriteRepository _productWrite;
        public ProductService(ProductReadRepository productRead,ProductWriteRepository productWrite) 
        {
            _productRead = productRead;
            _productWrite= productWrite;
        }
        public bool AddProduct(Product entity)
        {
           return _productWrite.Add(entity);
        }

        public Task<bool> AddProductAsync(Product entity)
        {
            return _productWrite.AddAsync(entity);
        }

        public bool DeleteProduct(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            return _productRead.Get(id);
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
