namespace TestTaskRoutesAudit.Models
{
    public class Passenger : Base
    {
        public int Person { get; set; }
        public int DestinationStation { get; set; }
        public bool IsActive { get; set; }
    }
}
