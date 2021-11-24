using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;

namespace BinixPro.Storage;


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
            .AddSingleton<RedisContext>();
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