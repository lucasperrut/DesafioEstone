using System;
using System.Collections.Generic;

namespace Stone.Domain.Entities
{
    public class City
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Temperature> Temperatures { get; set; }
    }
}
