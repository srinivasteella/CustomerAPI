using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
    class Repository<T> : IRepository<T> where T : class
    {
        IDbContext _ctx;
        public Repository(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task Add(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _ctx.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Get()
        {
            return _ctx.Set<T>();
        }
    }
}
