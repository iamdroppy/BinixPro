using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Binix.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Binix.Database
{
    internal class Repo<T> : IRepo<T> where T: BaseEntity
    {
        private readonly DbSet<T> _set;

        internal Repo(DbSet<T> set) => _set = set;
        protected virtual DbSet<T> Set() => _set;
        public T[] GetMany(Expression<Func<T, bool>> predicate) => predicate != null ? Set().Where(predicate).ToArray() : Set().ToArray();
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) 
            => (predicate != null ? await Set().FirstOrDefaultAsync(predicate, token) : await Set().FirstOrDefaultAsync(token)) ?? throw new NotFoundException(typeof(T), $"couldn't find this item.");
        
        public async Task<T> FindAsync(Guid id) 
            => await Set().FindAsync(id) ?? throw new NotFoundException(typeof(T), $"couldn't find this item.");

        public async Task AddAsync(T obj) => await Set().AddAsync(obj);
        public void Delete(T obj) => Set().Remove(obj);
        public void Delete(Guid obj)
        {
            Delete(FindAsync(obj).GetAwaiter().GetResult());
        }

        public void Update(T obj) => Set().Update(obj);
    }
}