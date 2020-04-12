using System.Collections.Generic;
using System.Linq;
using TestTaskRoutesAudit.Enums;

namespace TestTaskRoutesAudit.Services.EntityComparers
{
    public class StationComparerService : IEntityComparerService<Entities.Station>
    {
        public IList<Entities.Audit.AuditLogEntry> Compare(Entities.Station original, Entities.Station updated)
        {
            var auditLogEntries = new List<Entities.Audit.AuditLogEntry>();
            
            CompareOrder(auditLogEntries, original, updated);

            return auditLogEntries;
        }

        
        private void CompareOrder(
            IList<Entities.Audit.AuditLogEntry> auditLogEntries, Entities.Station original, Entities.Station updated)
        {
            if (original.Order != updated.Order)
            {
                auditLogEntries.Add(new Entities.Audit.AuditLogEntry
                {
                    OriginalValue = original.Order.ToString(),
                    NewValue = updated.Order.ToString(),
                    TypeOfChange = $"{(int)TypeOfChange.StationOrder} - {TypeOfChange.StationOrder}",
                    Planned = updated.Order == updated.PlannedOrder
                });
            }
        }
    }
}
