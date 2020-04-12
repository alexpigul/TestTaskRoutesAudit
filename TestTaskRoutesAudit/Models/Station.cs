using System.Collections.Generic;

namespace TestTaskRoutesAudit.Models
{
    public class Station : Base
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<int> Passengers { get; set; }
        public int Order { get; set; }
        public int PlannedOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
