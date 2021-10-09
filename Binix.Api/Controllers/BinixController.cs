using System;
using System.Threading.Tasks;
using Binix.Api.Model;
using Binix.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Binix.Api.Controllers
{
    public abstract class BinixController<T> : ControllerBase
        where T: BaseEntity
    {
        protected readonly ILogger<BinixController<T>> Logger;
        protected readonly IRepoTransactionBuilder TransactionBuilder;

        protected BinixController(ILogger<BinixController<T>> logger, IRepoTransactionBuilder transactionBuilder)
        {
            Logger = logger;
            TransactionBuilder = transactionBuilder;
        }

        [HttpGet()]
        public ActionResult<T[]> Get()
        {
            Logger.LogTrace($"{typeof(T).Name} :: GetMany()");
            try
            {
                return GetMany();
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"{typeof(T).Name} :: GetMany() : {ex.Message}", ex);
                throw;
            }
        }
        
        protected virtual ActionResult<T[]> GetMany()
        {
            using var transaction = TransactionBuilder.Build();
            return Ok(transaction.ReadOnly<T>().GetMany());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get([FromRoute] Guid id)
        {
            Logger.LogTrace($"{typeof(T).Name} :: Get({id})");
            try
            {
                return await GetAsync(id);
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"{typeof(T).Name} :: Get({id}) : {ex.Message}", ex);
                throw;
            }
        }

        protected virtual async Task<ActionResult<T>> GetAsync([FromRoute] Guid id)
        {
            await using var transaction = TransactionBuilder.Build();
            return Ok(await transaction.ReadOnly<T>().FindAsync(id));
        }
        
        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T obj)
        {
            Logger.LogTrace($"{typeof(T).Name} :: Post(...)");
            try
            {
                return await PostAsync(obj);
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"{typeof(T).Name} :: Post(...) : {ex.Message}", ex);
                throw;
            }
        }
        
        protected virtual async Task<ActionResult<T>> PostAsync(T obj)
        {
            await using var transaction = TransactionBuilder.Build();
            await transaction.Repo<T>().AddAsync(obj);
            await transaction.SaveChangesAsync();
            return Ok(obj.Id);
        }
        
        [HttpPut]
        public virtual async Task<ActionResult<T>> Put(T obj)
        {
            Logger.LogTrace($"{typeof(T).Name} :: Put(...)");
            try
            {
                return await PutAsync(obj);
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"{typeof(T).Name} :: Put(...) : {ex.Message}", ex);
                throw;
            }
        }

        protected virtual async Task<ActionResult<T>> PutAsync(T obj)
        {
            await using var transaction = TransactionBuilder.Build();
            transaction.Repo<T>().Update(obj);
            await transaction.SaveChangesAsync();
            return Ok(obj.Id);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete([FromRoute] Guid id)
        {
            Logger.LogTrace($"{typeof(T).Name} :: Delete(...)");
            try
            {
                return await DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"{typeof(T).Name} :: Delete({id}) : {ex.Message}", ex);
                throw;
            }
        }
        
        protected virtual async Task<ActionResult<T>> DeleteAsync(Guid id)
        {
            await using var transaction = TransactionBuilder.Build();
            transaction.Repo<T>().Delete(id);
            await transaction.SaveChangesAsync();
            return Ok(id);
        }
    }
}