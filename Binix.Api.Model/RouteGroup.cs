using System.Collections.Generic;

namespace Binix.Api.Model
{
    public class RouteGroup: BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}