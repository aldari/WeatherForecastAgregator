using System;

namespace Services.Core.Entities
{
    public class ForecastServiceEntity: Entity<int>
    {
        public virtual String Name { get; set; }
    }
}
