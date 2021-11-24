using System.ComponentModel.DataAnnotations;

namespace BinixPro.Database.Models;

public static class Route
{
    public class Request
    {
        [Required(ErrorMessage = "Friendly name is required")]
        [MinLength(3, ErrorMessage = "Friendly name must have at least 3 characters")]
        public string FriendlyName { get; set; } = string.Empty;
    }

    public record Response(Guid Id);
}