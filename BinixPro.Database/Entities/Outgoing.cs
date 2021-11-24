using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinixPro.Database.Entities;

public class Outgoing : IIdentifiable
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Group))]
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }

    public string Url { get; set; }
    public virtual ICollection<Group> Groups { get; set; }
}