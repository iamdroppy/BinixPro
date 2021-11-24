using System.ComponentModel.DataAnnotations;

namespace BinixPro.Storage;

/// <summary>
/// Options bound to IConfiguration
/// </summary>
internal class StorageOptions : IStorageOptions
{
    // TODO: Use IChangeToken
    public string Endpoint { get; set; } = string.Empty;
    public int DatabaseInstance { get; set; } = -1;
}

public interface IStorageOptions
{
    public string Endpoint { get; }
    public int Database { get; }
}