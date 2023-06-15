using BurgerCodeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Application.Interfaces.Repository.Services
{
    public interface IReadWriteService<T> where T : class 
    
    {
        public bool AddProduct(T entity);
        Task<bool> AddProductAsync(T entity);
        public bool DeleteProduct(T entity);
        Task<T> GetProductByIdAsync(int id);
        T GetProductById(int id);
        List<T> GetProducts();
    }
}
