using BinixPro.Database.Entities;
using BinixPro.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BinixPro.Database.Services;

public interface IProxyService
{
    Task<Models.Route.Response> CreateRouteAsync(Models.Route.Request request, CancellationToken token = default);
    Task<CreateCluster.Response> CreateHostAsync(CreateHost.Request request, CancellationToken token = default);
    Task<CreateCluster.Response> CreateClusterAsync(CreateCluster.Request request, CancellationToken token = default);
}

internal class ProxyService : IProxyService
{
    private readonly ILogger<ProxyService> _logger;
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public ProxyService(ILogger<ProxyService> logger, IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _logger = logger;
        _dbFactory = dbFactory;
    }

    public async Task<Models.Route.Response> CreateRouteAsync(Models.Route.Request request, CancellationToken token = default)
    {
        await using var db = _dbFactory.CreateDbContext();
        Entities.Route route = new()
        {
            FriendlyName = request.FriendlyName
        };

        await db.Routes.AddAsync(route, token);
        await db.SaveChangesAsync(token);

        return new Models.Route.Response(Guid.Empty);
    }

    public async Task<CreateCluster.Response> CreateHostAsync(CreateHost.Request request, CancellationToken token = default)
    {
        await using var db = _dbFactory.CreateDbContext();
        Host host = new()
        {
            RouteId = request.RouteId,
            Hostname = request.Hostname
        };

        await db.Hosts.AddAsync(host, token);
        await db.SaveChangesAsync(token);

        return new CreateCluster.Response(Guid.Empty);
    }
    public async Task<CreateCluster.Response> CreateClusterAsync(CreateCluster.Request request, CancellationToken token = default)
    {
        await using var db = _dbFactory.CreateDbContext();
        Cluster cluster = new()
        {
            RouteId = request.RouteId,
            Destination = request.Destination
        };

        await db.Clusters.AddAsync(cluster, token);
        await db.SaveChangesAsync(token);

        return new CreateCluster.Response(Guid.Empty);
    }
}
