using System.Collections.Generic;

namespace Binix.Api.Model
{
    public class Zone : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}