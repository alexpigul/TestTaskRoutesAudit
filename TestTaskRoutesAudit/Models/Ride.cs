using System.Collections.Generic;

namespace TestTaskRoutesAudit.Models
{
    public class Ride : Base
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string PlannedStartTime { get; set; }
        public IEnumerable<int> Stations { get; set; }
        public int? Driver { get; set; }
        public int PlannedDriver { get; set; }
        public bool Cancelled { get; set; }
    }
}
