using System;
using System.Collections.Generic;

namespace Software_Account_Management.Models;

public partial class Application
{
    public Guid ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public string? ApplicationVersion { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
