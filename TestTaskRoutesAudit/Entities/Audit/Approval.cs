
namespace TestTaskRoutesAudit.Entities
{
    public class Approval : BaseEntity
    {
        public Driver Driver { get; set; }
        public bool IsApproved { get; set; }
    }
}
