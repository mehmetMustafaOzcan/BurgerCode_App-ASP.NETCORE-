using BurgerCodeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerCodeApp.Application.Interfaces.Repository.ProductRepository
{
    public interface IProductReadRepository:IReadRepository<Product>
    {
    }
}
