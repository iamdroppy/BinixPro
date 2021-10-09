using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binix.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Binix.Database
{
    internal class RepoTransaction : IRepoTransaction
    {
        private readonly IServiceScope _scope;
        private readonly ApplicationDbContext _context;

        public RepoTransaction(IServiceProvider provider)
        {
            _scope = provider.CreateScope();
            _context = _scope.ServiceProvider.GetService<ApplicationDbContext>();
        }

        private readonly Dictionary<Type, object> _cachedList = new();
        public IRepo<T> Repo<T>(Func<DbSet<T>, DbSet<T>> defaultDbSet = null, string name = null) where T : BaseEntity
        {
            _disposer.ThrowIfDisposed();
            if (_cachedList.TryGetValue(typeof(T), out object outVal) && outVal is IRepo<T> cachedObject)
                return cachedObject;
            
            var dbSet = name != null ? _context.Set<T>(name) : _context.Set<T>();
            dbSet = (defaultDbSet?.Invoke(dbSet) ?? dbSet);
            var result = new Repo<T>(dbSet);
            _cachedList.Add(typeof(T), result);
            return result;
        }
        public IReadOnlyRepo<T> ReadOnly<T>(Func<DbSet<T>,DbSet<T>> defaultDbSet = null, string name = null) where T : BaseEntity
        {
            return Repo<T>((t) => { defaultDbSet?.Invoke(t); return t.AsNoTracking() as DbSet<T>;}, name);
        }

        public bool HasChanges() => _context.ChangeTracker.HasChanges();

        public async Task<bool> SaveChangesAsync(CancellationToken token = default)
        {
            _disposer.ThrowIfDisposed();
            return (await _context.SaveChangesAsync(token)) > 0;
        }

        public bool SaveChanges(CancellationToken token = default)
        {
            _disposer.ThrowIfDisposed();
            return _context.SaveChanges() > 0;
        }

        private Once _disposer = Once.Configure();
        public void Dispose()
        {
            if (!_disposer.IsDisposed())
            {
                _disposer.Dispose();
                _scope.Dispose();
                _context.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposer.IsDisposed())
            {
                _disposer.Dispose();
                _scope.Dispose();
                await _context.DisposeAsync();
            }
        }
    }
}