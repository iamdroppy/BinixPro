using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Binix.Api.Model
{
    public class Cluster : BaseEntity
    {
        public string Hostname { get; set; }
        public virtual ICollection<RouteGroup> Routes { get; set; }
        [ForeignKey(nameof(Zone))]
        public Guid ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
        public int? MaxConnectionsPerServer = null;
        public int? RequestTimeout = null;
    }
}