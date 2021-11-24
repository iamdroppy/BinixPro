using System.ComponentModel.DataAnnotations;

namespace BinixPro.Database.Models;

public static class CreateCluster
{
    public class Request
    {
        public Guid RouteId { get; set; }
        [Url] public string Destination { get; set; } = string.Empty;
    }

    public record Response(Guid Id);
}