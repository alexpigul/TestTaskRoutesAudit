using System;

namespace TestTaskRoutesAudit.Enums
{
    [Flags]
    public enum Days
    {
        Sunday = 0x0,
        Monday = 0x1,
        Tuesday = 0x2,
        Wednesday = 0x4,
        Thursday = 0x8,
        Friday = 0x10,
        Saturday = 0x20
    }
}
