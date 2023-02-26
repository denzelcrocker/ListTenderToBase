using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class Act
{
    public int ActId { get; set; }

    public string? NameOfAct { get; set; }

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}
