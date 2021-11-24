using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BinixPro.Database;

public static class BinixDbExtensions
{
    // TODO: more database providers
    public static IServiceCollection AddBinixDb(this IServiceCollection services, string sqliteDb)
    {
        return services.AddDbContextFactory<ApplicationDbContext>(cfg => cfg.UseSqlite("Data Source=" + sqliteDb));
    }
}