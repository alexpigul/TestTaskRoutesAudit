using System.Collections.Generic;

namespace TestTaskRoutesAudit.Entities
{
    public class Ride : BaseEntity
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string PlannedStartTime { get; set; }
        public ICollection<Station> Stations { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int PlannedDriverId { get; set; }
        public Driver PlannedDriver { get; set; }
        public bool Cancelled { get; set; }
    }
}
