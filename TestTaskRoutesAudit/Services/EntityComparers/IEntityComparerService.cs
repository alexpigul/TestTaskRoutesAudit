using System.Collections.Generic;
using TestTaskRoutesAudit.Entities;

namespace TestTaskRoutesAudit.Services.EntityComparers
{
    public interface IEntityComparerService<in T> where T : BaseEntity
    {
        public IList<Entities.Audit.AuditLogEntry> Compare(T original, T updated);
    }
}
