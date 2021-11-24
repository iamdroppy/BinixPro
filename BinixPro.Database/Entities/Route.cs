using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinixPro.Database.Entities
{
    public class Route
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Friendly name is required")]
        [MinLength(3, ErrorMessage = "Friendly name must have at least 3 characters")]
        public string FriendlyName { get; set; } = string.Empty;

        public virtual ICollection<Host> Hosts { get; set; } = new List<Host>();
        public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();

        [Required] public DateTime IncludedAt { get; set; } = DateTime.UtcNow;

        [NotMapped] public int MaxConnectionsPerServer { get; set; } = 1000;
        [NotMapped] public bool RequestHeadersCopy { get; set; } = true;
        [NotMapped] public bool RequestHeaderOriginalHost { get; set; } = true;

    }
}