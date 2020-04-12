using System.Collections.Generic;
using System.Linq;
using TestTaskRoutesAudit.Enums;

namespace TestTaskRoutesAudit.Services.EntityComparers
{
    public class RideComparerService : IEntityComparerService<Entities.Ride>
    {
        private readonly IEntityComparerService<Entities.Station> _stationComparerService;

        public RideComparerService(IEntityComparerService<Entities.Station> stationComparerService)
        {
            _stationComparerService = stationComparerService;
        }

        public IList<Entities.Audit.AuditLogEntry> Compare(Entities.Ride original, Entities.Ride updated)
        {
            var auditLogEntries = new List<Entities.Audit.AuditLogEntry>();

            CompareDate(auditLogEntries, original, updated);

            CompareStartTime(auditLogEntries, original, updated);
            
            CompareStations(auditLogEntries, original, updated);
            
            CompareDriver(auditLogEntries, original, updated);

            return auditLogEntries;
        }


        private void CompareDate(
            IList<Entities.Audit.AuditLogEntry> auditLogEntries, Entities.Ride original, Entities.Ride updated)
        {
            if (!original.Date.Equals(updated.Date))
            {
                auditLogEntries.Add(new Entities.Audit.AuditLogEntry
                {
                    OriginalValue = string.Join(';', original.Date),
                    NewValue = string.Join(';', updated.Date),
                    TypeOfChange = $"{(int)TypeOfChange.RideDate} - {TypeOfChange.RideDate}",
                    Planned = false
                });
            }
        }

        private void CompareStartTime(
            IList<Entities.Audit.AuditLogEntry> auditLogEntries, Entities.Ride original, Entities.Ride updated)
        {
            if (!original.StartTime.Equals(updated.StartTime))
            {
                auditLogEntries.Add(new Entities.Audit.AuditLogEntry
                {
                    OriginalValue = string.Join(';', original.StartTime),
                    NewValue = string.Join(';', updated.StartTime),
                    TypeOfChange = $"{(int)TypeOfChange.RideStartTime} - {TypeOfChange.RideStartTime}",
                    Planned = updated.StartTime.Equals(updated.PlannedStartTime)
                });
            }
        }

        private void CompareStations(
            List<Entities.Audit.AuditLogEntry> auditLogEntries, Entities.Ride original, Entities.Ride updated)
        {
            IList<int> originalStationIds = original.Stations.Select(x => x.Id).ToList();
            IList<int> updatedStationsIds = updated.Stations.Select(x => x.Id).ToList();

            if (originalStationIds.Except(updatedStationsIds).Any())
            {
                auditLogEntries.Add(new Entities.Audit.AuditLogEntry
                {
                    OriginalValue = string.Join(';', originalStationIds),
                    NewValue = string.Join(';', updatedStationsIds),
                    TypeOfChange = $"{(int)TypeOfChange.RideStations} - {TypeOfChange.RideStations}",
                    Planned = false
                });
            }
            
            IList<Entities.Station> originalStations = original.Stations.OrderBy(x => x.Name).ToList();
            IList<Entities.Station> updateStations = original.Stations.OrderBy(x => x.Name).ToList();

            if (originalStations.Any() && updateStations.Any())
            {
                for (int i = 0; i < originalStations.Count && i < updateStations.Count; i++)
                {
                    auditLogEntries.AddRange(_stationComparerService.Compare(originalStations[i], updateStations[i]));
                }
            }
        }


        private void CompareDriver(
            IList<Entities.Audit.AuditLogEntry> auditLogEntries, Entities.Ride original, Entities.Ride updated)
        {
            if (original.DriverId != updated.DriverId)
            {
                auditLogEntries.Add(new Entities.Audit.AuditLogEntry
                {
                    OriginalValue = string.Join(';', original.DriverId),
                    NewValue = string.Join(';', updated.DriverId),
                    TypeOfChange = $"{(int)TypeOfChange.RideDriver} - {TypeOfChange.RideDriver}",
                    Planned = updated.DriverId == updated.PlannedDriverId
                });
            }
        }

        
    }
}
