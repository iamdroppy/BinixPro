using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Binix.Api.Model
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [NotMapped] public bool HasBeenUpdated => CreatedAt == UpdatedAt;
    }
}