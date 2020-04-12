using TestTaskRoutesAudit.Models.InputJson;

namespace TestTaskRoutesAudit.Models
{
    public class Approval : Base
    {
        public int Driver { get; set; }
        public bool IsApproved { get; set; }
    }
}
