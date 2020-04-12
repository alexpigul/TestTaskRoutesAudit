using System.Collections.Generic;
using TestTaskRoutesAudit.Enums;

namespace TestTaskRoutesAudit.Entities.Audit
{
    public class AuditLogEntry
    {
        public string StartDateOfChange { get; set; }
        public string EndDateOfChange { get; set; }
        public Days AffectedDays { get; set; }
        public string TypeOfChange { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public bool Planned { get; set; }
        public IList<Approval> ListOfApprovals { get; set; }
    }
}
