using Microsoft.Extensions.DependencyInjection;

namespace Binix.Database
{
    public static class ServiceExpose
    {
        public static IServiceCollection AddDb(this IServiceCollection services)
        {
            return services
                .AddTransient<ApplicationDbContext>()
                .AddScoped(typeof(IRepo<>), typeof(Repo<>))
                .AddTransient<IRepoTransactionBuilder, RepoTransactionBuilder>()
                .AddTransient<IRepoTransaction, RepoTransaction>();
        }
    }
}