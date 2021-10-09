using System;
using System.Threading.Tasks;
using Binix.Api.Model;

namespace Binix.Database
{
    public interface IRepo<T> : IReadOnlyRepo<T>
        where T : BaseEntity
    {
        Task AddAsync(T obj);
        void Delete(T obj);
        void Delete(Guid obj);
        void Update(T obj);
    }
}