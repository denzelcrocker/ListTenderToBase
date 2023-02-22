using System;
using System.Collections.Generic;

namespace ListTenderToBase;

public partial class Procurement
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public string? Address { get; set; }

    public string? Method { get; set; }

    public string? Act { get; set; }

    public int? PlatformId { get; set; }

    public DateTime? DeadlineStart { get; set; }

    public DateTime? DeadlineEnd { get; set; }

    public int? TimeZoneId { get; set; }

    public decimal? InitialPrice { get; set; }

    public int? OrganizationId { get; set; }

    public string? ProcurementObject { get; set; }

    public string? PlaceOfDelivery { get; set; }

    public string? SupplyAssurance { get; set; }

    public string? Enforcement { get; set; }

    public string? ProvidingAguarantee { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Platform? Platform { get; set; }

    public virtual TimeZone? TimeZone { get; set; }
}
