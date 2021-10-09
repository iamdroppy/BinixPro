using System;
using Microsoft.Extensions.DependencyInjection;

namespace Binix.Database
{
    class RepoTransactionBuilder : IRepoTransactionBuilder
    {
        private readonly IServiceScope _scope;
        public RepoTransactionBuilder(IServiceProvider provider) => _scope = provider.CreateScope();
        public IRepoTransaction Build() => _scope.ServiceProvider.GetRequiredService<IRepoTransaction>();
    }
}