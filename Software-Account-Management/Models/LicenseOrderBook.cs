using System;
using System.Collections.Generic;

namespace Software_Account_Management.Models;

public partial class LicenseOrderBook
{
    public Guid LicenseOrderBookId { get; set; }

    public string TestStationName { get; set; } = null!;

    public int TestCaseId { get; set; }

    public string Orchestrator { get; set; } = null!;

    public DateTime ReservationTime { get; set; }

    public DateTime EstCompletionTime { get; set; }

    public DateTime? CompletionTime { get; set; }

    public string ReservedBy { get; set; } = null!;

    public string Framework { get; set; } = null!;

    public Guid LicenseId { get; set; }

    public string TestStationPool { get; set; } = null!;

    public int SpaceId { get; set; }

    public virtual License License { get; set; } = null!;
}
