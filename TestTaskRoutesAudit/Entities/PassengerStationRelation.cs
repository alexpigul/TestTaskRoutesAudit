namespace TestTaskRoutesAudit.Entities
{
    public class PassengerStationRelation
    {
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int StationId { get; set; }
        public Station Station { get; set; }
    }
}
