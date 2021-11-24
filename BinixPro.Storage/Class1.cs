using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;

namespace BinixPro.Storage;

class Host
{
    [Key]
    public Guid Id { get; set; }
    [Required, MinLength(1)] public string Hostname { get; set; } = string.Empty;
}

class Cluster
{
    [Key] public Guid Id { get; set; }
    [ForeignKey(nameof(Host))] public Guid HostId { get; set; }
    [Required, MinLength(1)] public string SubDomain { get; set; } = string.Empty;

    public virtual Host Host { get; set; }
    public virtual ICollection<Route> Routes { get; set; }
}

class Route
{
    [Key] public Guid Id { get; set; }
    [ForeignKey(nameof(Cluster))] public Guid ClusterId { get; set; }
    [Required, MinLength(1)] public string ForwardUrl { get; set; } = string.Empty;

    /// <summary>
    /// If set to false, the hostname will match the Cluster's subdomain and Host's hostname,
    /// otherwise, it will use ForwardUrl's host
    /// </summary>
    public bool RealHostname { get; set; } = true;

    public virtual Cluster Cluster { get; set; }
}


public class InvalidConfigurationException : ArgumentException
{
    public InvalidConfigurationException(string message) : base(message)
    {
        
    }
}

public static class StorageExtensions
{
    public static IServiceCollection AddRedisStorage(this IServiceCollection services, [NotNull] IConfiguration configuration)
    {
        if (Equals(configuration, null))
            throw new ArgumentNullException(nameof(configuration));
        
        var storageOptions = new StorageOptions();
        configuration.Bind(storageOptions);

        if (!storageOptions.Endpoint.Any())
            throw new InvalidOperationException("No endpoints has been configured.");

        return services
            .AddSingleton<IStorageOptions>(storageOptions)
            .AddScoped<RedisContext>();
    }
}

public class RedisContext : IDisposable
{
    private readonly IStorageOptions _storageOptions;
    private readonly ConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    public RedisContext(IStorageOptions storageOptions)
    {
        _storageOptions = storageOptions;

        _connectionMultiplexer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = {storageOptions.Endpoint},
                AbortOnConnectFail = true
            });

        _database = _connectionMultiplexer.GetDatabase(storageOptions.Database);
    }

    public async Task SetAsync<T>([NotNull] string key, [NotNull] T obj) => await _database.StringSetAsync(key, JsonSerializer.Serialize(obj ?? throw new ArgumentNullException(nameof(obj))));
    public async Task GetAsync<T>([NotNull] string key) => JsonSerializer.Deserialize<T>(await _database.StringGetAsync(key));

    public void Dispose()
    {
        _connectionMultiplexer.Dispose();
    }
}