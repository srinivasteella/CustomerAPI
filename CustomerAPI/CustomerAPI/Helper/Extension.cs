using CustomerAPI.Repository;
using CustomerAPI.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAPI.Helper
{
    public static class Extension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(db => db.UseInMemoryDatabase("CustomerDB"));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDbContext, DataContext>();
            services.AddScoped<IDBManager, DBManager>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
