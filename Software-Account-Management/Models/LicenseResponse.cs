namespace Software_Account_Management.Models
{
    public class LicenseResponse
    {
        public Guid LicenseId { get; set; }
        public required String ApplicationName { get; set; }
        public String? ApplicationVersion { get; set; }
        public required String LicenseVendor { get; set; }
        public required String SpaceName { get; set; }
        public List<String>? TestStationPool { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime? ExpiredOn { get; set; }
        //we will postfill after the creation
        public LicenseOrderBook? Reservation { get; set; }

        public static explicit operator LicenseResponse(License license)
        {
            var tsp = string.IsNullOrEmpty(license.TestStationPool)
                ? new List<string>()
                : license.TestStationPool.Split(':', StringSplitOptions.RemoveEmptyEntries)
                                           .Select(item => item.Trim())
                                           .ToList();

            return new LicenseResponse
            {
                LicenseId = license.LicenseId,
                ApplicationName = license.Application.ApplicationName,
                ApplicationVersion = license.Application.ApplicationVersion,
                LicenseVendor = license.LicenseVendor.VendorName,
                SpaceName = license.SpaceId.ToString(),
                TestStationPool = tsp,
                UserName = license.UserName,
                Password = license.Password,
                CreatedBy = license.CreatedBy,
                LastModified = license.LastModified,
                ExpiredOn = license.ExpiredOn
            };
        }
    }

}
