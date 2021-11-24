using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinixPro.Database.Entities
{
    public class Host
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Route))]
        public Guid RouteId { get; set; }
        public Route? Route { get; set; }

        public string Hostname { get; set; } = string.Empty;
    }
}