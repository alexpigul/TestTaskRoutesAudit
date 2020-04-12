
namespace TestTaskRoutesAudit.Entities
{
    public class Passenger : BaseEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int DestinationStationId { get; set; }
        public bool IsActive { get; set; }
    }
}
