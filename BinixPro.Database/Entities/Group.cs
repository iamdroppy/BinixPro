using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinixPro.Database.Entities;

public class Group : IIdentifiable
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    [ForeignKey(nameof(Zone))]
    public Guid ZoneId { get; set; }
    public virtual Zone Zone { get; set; }

    public virtual ICollection<Incoming> Incomings { get; set; }
    public virtual ICollection<Outgoing> Outgoings { get; set; }
}