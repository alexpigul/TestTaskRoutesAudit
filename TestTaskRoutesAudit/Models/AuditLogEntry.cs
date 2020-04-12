using System.Collections.Generic;
using TestTaskRoutesAudit.Enums;

namespace TestTaskRoutesAudit.Models
{
    public class AuditLogEntry
    {
        public string StartDateOfChange { get; set; }
        public string EndDateOfChange { get; set; }
        public Days AffectedDays { get; set; }
        public string TypeOfChange { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public IEnumerable<Approval> ListOfApprovals { get; set; }
    }
}
