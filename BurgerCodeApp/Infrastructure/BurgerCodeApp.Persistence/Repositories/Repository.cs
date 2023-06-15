using BurgerCodeApp.Application.Interfaces.Repository;
using BurgerCodeApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BurgerDbContext _context;

        public Repository(BurgerDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table =>_context.Set<T>();
    }
}
