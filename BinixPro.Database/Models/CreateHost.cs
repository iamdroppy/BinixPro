namespace BinixPro.Database.Models;

public static class CreateHost
{
    public class Request
    {
        public Guid RouteId { get; set; }
        public string Hostname { get; set; } = string.Empty;
    }

    public record Response(Guid Id);
}