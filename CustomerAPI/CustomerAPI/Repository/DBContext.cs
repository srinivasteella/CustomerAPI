
using CustomerAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CustomerAPI.Repository
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveAsync();
    }

    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
