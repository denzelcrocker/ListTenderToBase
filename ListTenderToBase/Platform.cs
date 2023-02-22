using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class Platform
{
    public int PlatformId { get; set; }

    public string? NameOfPlatform { get; set; }

    public string? AddressOfPlatform { get; set; }

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}
