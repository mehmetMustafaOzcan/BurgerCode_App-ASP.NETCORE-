using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Application.Interfaces.Repository
{
    public interface IReadRepository<T>:IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);

        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> GetByKeysAsync(params object?[]? keyValues);
    }
}
