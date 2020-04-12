using System.Collections.Generic;
using System.Linq;

namespace TestTaskRoutesAudit.Services.EntityComparers
{
    public class RouteComparerService : IEntityComparerService<Entities.Route>
    {
        private readonly IEntityComparerService<Entities.Ride> _rideComparerService;

        public RouteComparerService(IEntityComparerService<Entities.Ride> rideComparerService)
        {
            _rideComparerService = rideComparerService;
        }

        public IList<Entities.Audit.AuditLogEntry> Compare(Entities.Route original, Entities.Route updated)
        {
            var auditLogEntries = new List<Entities.Audit.AuditLogEntry>();

            IList<Entities.Ride> originalRides = original.Rides.ToList();
            IList<Entities.Ride> updatedRides = updated.Rides.ToList();

            for (int i = 0; i < originalRides.Count && i < updatedRides.Count; i++)
            {
                auditLogEntries.AddRange(_rideComparerService.Compare(originalRides[i], updatedRides[i]));
            }

            return auditLogEntries;
        }
    }
}
