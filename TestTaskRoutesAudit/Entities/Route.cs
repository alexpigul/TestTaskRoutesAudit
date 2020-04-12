using System.Collections.Generic;
using TestTaskRoutesAudit.Enums;

namespace TestTaskRoutesAudit.Entities
{
    public class Route : BaseEntity
    {
        public string Name { get; set; }
        public Days ActiveDays { get; set; }
        public ICollection<Ride> Rides { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
