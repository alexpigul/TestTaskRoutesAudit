using System.Collections.Generic;
using TestTaskRoutesAudit.Models;

namespace TestTaskRoutesAudit.Entities.Context
{
    public class EntityContext
    {
        public IList<Route> Routes { get; set; }
        public IList<Ride> Rides { get; set; }
        public IList<Station> Stations { get; set; }
        public IList<Passenger> Passengers { get; set; }
        public IList<PassengerStationRelation> PassengerStationRelations { get; set; }
        public IList<Driver> Drivers { get; set; }
        public IList<Person> Persons { get; set; }

        public IList<AuditLogEntry> AuditLogEntries { get; set; }
        public IList<Approval> Approvals { get; set; }
    }
}
