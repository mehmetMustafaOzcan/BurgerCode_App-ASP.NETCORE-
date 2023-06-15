using BurgerCodeApp.Application.Interfaces.Repository.ProductRepository;
using BurgerCodeApp.Application.Interfaces.Repository.Services;
using BurgerCodeApp.Persistence.Concretes;
using BurgerCodeApp.Persistence.Repositories.ProductRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerCodeApp.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IReadWriteService<dynamic>, ReadWriteService<dynamic>>();

        }
    }
}
