using BurgerCodeApp.Application.Interfaces.Repository;
using BurgerCodeApp.Application.Interfaces.Repository.Services;
using BurgerCodeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Persistence.Concretes
{
    public class ReadWriteService<T> : IReadWriteService<T> where T : class
    {
        private readonly IReadRepository<T> _readRepository;

        private readonly IWriteRepository<T> _writeRepository;
        public ReadWriteService(IReadRepository<T> readRepository, IWriteRepository<T> writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public bool AddProduct(T entity)
        {
           return _writeRepository.Add(entity);
        }

        public async Task<bool> AddProductAsync(T entity)
        {
            return await _writeRepository.AddAsync(entity);
        }

        public bool DeleteProduct(T entity)
        {
           return _writeRepository.Remove(entity);
        }

        public T GetProductById(int id)
        {
            return _readRepository.Get(id);
        }

        public async Task<T> GetProductByIdAsync(int id)
        {
           return await _readRepository.GetByKeysAsync(id);
        }

        public List<T> GetProducts()
        {
            return _readRepository.GetAll().ToList();
        }
    }
}
