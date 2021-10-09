using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Binix.Database
{
    public interface IReadOnlyRepo<T>
    {
        T[] GetMany(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null, CancellationToken token = default);
        Task<T> FindAsync(Guid id);
    }
}