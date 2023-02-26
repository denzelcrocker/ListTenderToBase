using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class Method
{
    public int MethodId { get; set; }

    public string? NameOfMethod { get; set; }

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}
