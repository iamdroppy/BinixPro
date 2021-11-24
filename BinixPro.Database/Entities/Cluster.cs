using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinixPro.Database.Entities
{
    public class Cluster
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Route))]
        public Guid RouteId { get; set; }
        public Route? Route { get; set; }

        [Url] public string Destination { get; set; } = string.Empty;
    }
}