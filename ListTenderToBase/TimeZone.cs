using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class TimeZone
{
    public int TimeZoneId { get; set; }

    public short? Code { get; set; }
    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}
