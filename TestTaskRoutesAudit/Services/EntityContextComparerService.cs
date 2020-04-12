using System.Collections.Generic;
using System.Linq;
using TestTaskRoutesAudit.Services.EntityComparers;

namespace TestTaskRoutesAudit.Services
{
    public interface IEntityContextComparerService
    {
        IList<Entities.Audit.AuditLogEntry> CompareContextsAndGetAudits(
            Entities.Context.EntityContext originalContext, Entities.Context.EntityContext updatedContext);
    }

    public class EntityContextComparerService : IEntityContextComparerService
    {
        private readonly IEntityComparerService<Entities.Route> _routeComparerService;

        public EntityContextComparerService(
            IEntityComparerService<Entities.Route> routeComparerService)
        {
            _routeComparerService = routeComparerService;
        }
        

        public IList<Entities.Audit.AuditLogEntry> CompareContextsAndGetAudits(
            Entities.Context.EntityContext originalContext, Entities.Context.EntityContext updatedContext)
        {

            List<Entities.Audit.AuditLogEntry> auditLogEntries = new List<Entities.Audit.AuditLogEntry>();

            for (int i =0; i < originalContext.Routes.Count && i < updatedContext.Routes.Count; i++)
            {
                auditLogEntries.AddRange(_routeComparerService.Compare(originalContext.Routes[i],
                    updatedContext.Routes[i]));
            }

            auditLogEntries = auditLogEntries
                .GroupBy(x => new
                {
                    x.TypeOfChange,
                    x.Planned,
                    x.OriginalValue,
                    x.NewValue
                })
                .Select(x => x.ToList())
                .SelectMany(x => x).ToList();

            return auditLogEntries;
        }
    }
}
