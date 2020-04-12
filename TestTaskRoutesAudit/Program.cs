using TestTaskRoutesAudit.Services;
using TestTaskRoutesAudit.Services.EntityComparers;

namespace TestTaskRoutesAudit
{
    class Program
    {
        static void Main(string[] args)
        {
            IJsonFileService jsonFileService = new JsonFileService();
            IJsonToEntityConverter jsonToEntityConverter = new JsonToEntityConverter();

            IEntityComparerService<Entities.Station> stationComparerService = new StationComparerService();
            IEntityComparerService<Entities.Ride> rideComparerService = new RideComparerService(stationComparerService);
            IEntityComparerService<Entities.Route> routeComparerService = new RouteComparerService(rideComparerService);
            IEntityContextComparerService entityContextComparerService = new EntityContextComparerService(routeComparerService);


            IAuditManagerService auditManagerService = new AuditManagerService(jsonFileService, jsonToEntityConverter, entityContextComparerService);

            string originalJsonPath = @"Jsons\Original.json";
            string updatedJsonPath = @"Jsons\Updated.json";
            string outputJsonPath = @"Jsons\OutputAudit.json";

            auditManagerService.CompareRoutesAndGetAudit(originalJsonPath, updatedJsonPath, outputJsonPath);
        }
    }
}
