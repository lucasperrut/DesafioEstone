using System;

namespace Stone.Domain.Entities
{
    public class Temperature
    {
        public Guid ID { get; set; }
        public Guid CityID { get; set; }
        public DateTime Date { get; set; }
        public string Measure { get; set; }
        public virtual City City { get; set; }
    }
}