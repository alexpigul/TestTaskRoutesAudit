namespace TestTaskRoutesAudit.Entities
{
    public class Driver : BaseEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string LicenseNumber { get; set; }
    }
}
