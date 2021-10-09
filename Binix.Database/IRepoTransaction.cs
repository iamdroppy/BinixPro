using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binix.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Binix.Database
{
    public interface IRepoTransaction : IDisposable, IAsyncDisposable
    {
        IReadOnlyRepo<T> ReadOnly<T>(Func<DbSet<T>, DbSet<T>> defaultDbSet = null, string name = null) where T : BaseEntity;
        IRepo<T> Repo<T>(Func<DbSet<T>, DbSet<T>> defaultDbSet = null, string name = null) where T : BaseEntity;
        bool HasChanges();
        Task<bool> SaveChangesAsync(CancellationToken token = default);
        bool SaveChanges(CancellationToken token = default);
    }
}