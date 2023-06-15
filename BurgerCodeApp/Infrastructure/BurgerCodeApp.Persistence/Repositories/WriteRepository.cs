using BurgerCodeApp.Application.Interfaces.Repository;
using BurgerCodeApp.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Persistence.Repositories
{
    public class WriteRepository<T> : Repository<T>, IWriteRepository<T> where T : class
    {
        public WriteRepository(BurgerDbContext context) : base(context)
        {
        }

        public bool Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
