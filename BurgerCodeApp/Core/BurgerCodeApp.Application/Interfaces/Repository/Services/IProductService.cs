using BurgerCodeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Application.Interfaces.Repository.Services
{
    public interface IProductService
    {
        public bool AddProduct(Product entity);
        Task<bool> AddProductAsync(Product entity);
        public bool DeleteProduct(Product entity);
        Task<Product> GetProductByIdAsync(int id);
        Product GetProductById(int id);
        List<Product> GetProducts();
    }
}
