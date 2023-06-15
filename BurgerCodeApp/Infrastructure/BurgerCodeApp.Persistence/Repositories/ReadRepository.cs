using BurgerCodeApp.Application.Interfaces.Repository;
using BurgerCodeApp.Persistence.Context;
using System.Linq.Expressions;

namespace BurgerCodeApp.Persistence.Repositories
{
    public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : class
    {
        public ReadRepository(BurgerDbContext context):base(context) { }
        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByKeysAsync(params object?[]? keyValues)
        {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
