using BurgerCodeApp.Application.Interfaces.Repository.ProductRepository;
using BurgerCodeApp.Domain.Entities;
using BurgerCodeApp.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Persistence.Repositories.ProductRepository
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(BurgerDbContext context) : base(context)
        {
        }
    }
}
