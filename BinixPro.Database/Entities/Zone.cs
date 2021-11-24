using System.ComponentModel.DataAnnotations;

namespace BinixPro.Database.Entities;

public class Zone : IIdentifiable
{
    [Key]
    public Guid Id { get; set; }

    public string Hostname { get; set; }

    public virtual ICollection<Group> Groups { get; set; }
}