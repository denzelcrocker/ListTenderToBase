using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string? NameOfOrganization { get; set; }

    public string? AddressOfOrganisation { get; set; }

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}
