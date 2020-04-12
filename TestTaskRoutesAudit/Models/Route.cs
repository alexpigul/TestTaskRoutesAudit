using System.Collections.Generic;

namespace TestTaskRoutesAudit.Models
{
    public class Route : Base
    {
        public string Name { get; set; }
        public string ActiveDays { get; set; }
        public IEnumerable<int> Rides { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
