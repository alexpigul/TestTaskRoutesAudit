using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskRoutesAudit.Models;

namespace TestTaskRoutesAudit.Services
{
    public interface IAuditManagerService
    {
        Task CompareRoutesAndGetAudit(string originalJsonPath, string updatedJsonPath, string outputJsonPath);
    }


    public class AuditManagerService : IAuditManagerService
    {
        private readonly IJsonFileService _jsonFileService;
        private readonly IJsonToEntityConverter _jsonToEntityConverter;
        private readonly IEntityContextComparerService _entityContextComparerService;

        public AuditManagerService(
            IJsonFileService jsonFileService,
            IJsonToEntityConverter jsonToEntityConverter,
            IEntityContextComparerService entityContextComparerService)
        {
            _jsonFileService = jsonFileService;
            _jsonToEntityConverter = jsonToEntityConverter;
            _entityContextComparerService = entityContextComparerService;
        }

        public async Task CompareRoutesAndGetAudit(string originalJsonPath, string updatedJsonPath, string outputJsonPath)
        {
            Models.InputJson.Json originalJson = await _jsonFileService.ReadJsonFileAsync(originalJsonPath);
            Models.InputJson.Json updatedJson = await _jsonFileService.ReadJsonFileAsync(updatedJsonPath);

            Entities.Context.EntityContext originalContext = _jsonToEntityConverter.ConvertJsonRouteToEntityContext(originalJson);
            Entities.Context.EntityContext updatedContext = _jsonToEntityConverter.ConvertJsonRouteToEntityContext(updatedJson);
            
            IList<Entities.Audit.AuditLogEntry> auditLogEntries =
                _entityContextComparerService.CompareContextsAndGetAudits(originalContext, updatedContext);
            
            Models.OutputJson.OutputJson outputJson = MapAuditEntitiesToOutputJson(auditLogEntries);

            await _jsonFileService.WriteJsonFileAsync(outputJson, originalJsonPath);
        }


        private Models.OutputJson.OutputJson MapAuditEntitiesToOutputJson(IList<Entities.Audit.AuditLogEntry> auditLogEntries)
        {
            Models.OutputJson.OutputJson outputJson = new Models.OutputJson.OutputJson
            {
                AuditLogEntries = auditLogEntries.Select(x => new AuditLogEntry
                {
                    StartDateOfChange = x.StartDateOfChange,
                    EndDateOfChange = x.EndDateOfChange,
                    TypeOfChange = x.TypeOfChange,
                    AffectedDays = x.AffectedDays,
                    NewValue = x.NewValue,
                    OriginalValue = x.OriginalValue,
                    ListOfApprovals = x.ListOfApprovals.Select(a => new Approval
                    {
                        Id = a.Id, Driver = a.Driver.Id, IsApproved = a.IsApproved
                    })
                })
            };

            return outputJson;
        }
    }
}
