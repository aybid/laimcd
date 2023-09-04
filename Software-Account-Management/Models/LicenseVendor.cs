using System;
using System.Collections.Generic;

namespace Software_Account_Management.Models;

public partial class LicenseVendor
{
    public Guid LicenseVendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
