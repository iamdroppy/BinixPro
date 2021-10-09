using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Binix.Api.Model;
using Binix.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Binix.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainController : BinixController<Zone>
    {
        public DomainController(ILogger<BinixController<Zone>> logger, IRepoTransactionBuilder transactionBuilder) : base(logger, transactionBuilder)
        {}

        protected override async Task<ActionResult<Zone>> GetAsync(Guid id)
        {
            await using var transaction = TransactionBuilder.Build();
            return Ok(await transaction.ReadOnly<Zone>(s=>s.Include(s=>s.Clusters).ThenInclude(s=>s.Routes)).FindAsync(id));
        }
    }
}