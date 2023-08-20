﻿namespace Software_Account_Management.Models
{
    public class LicenseQueue
    {
        public int Id { get; set; }
        public Guid AppLicenseId { get; set; }
        public AppLicense AppLicense { get; set; } = null!;
        public required LicenseOrderBook Reservation { get; set; }
    }
}
