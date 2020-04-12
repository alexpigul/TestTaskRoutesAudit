using System.Collections.Generic;

namespace TestTaskRoutesAudit.Models.OutputJson
{
    public class OutputJson
    {
        public IEnumerable<AuditLogEntry> AuditLogEntries { get; set; }
    }
}
