using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Binix.Api.Model
{
    public class Route : BaseEntity
    {
        [Protocol, Required]
        public string Protocol { get; set; }
        [Required]
        public string Hostname { get; set; }
        public string Path { get; set; }
        
        [ForeignKey(nameof(RouteGroup))] public Guid RouteGroupId { get; set; }
        public virtual RouteGroup RouteGroup { get; set; }
        
        
        [ForeignKey(nameof(Header))] public Guid HeaderId { get; set; }
        public virtual Header Header { get; set; }
    }

    public enum HeaderRoutingType
    {
        ExactHeader,
        HeaderPrefix,
        Exists
    }
    public class Header : BaseEntity
    {
        [ForeignKey(nameof(Route))]
        public Guid RouteId { get; set; }
        public Route Route { get; set; }
        
        public HeaderRoutingType RoutingType { get; set; }
        
        [MinLength(1), MaxLength(16)]
        public string Key { get; set; }
        
        /// <summary>
        /// Json value - if routing type is Exists this is ignored.
        /// </summary>
        public string Values { get; set; }
    }
}