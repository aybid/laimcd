namespace Software_Account_Management.Models
{
    public class AppLicenseRequest
    {
        public required string AppName { get; set; }
        public required string AppService { get; set; }
        public int SpaceId { get; set; }
        public required string TestStationPool { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public bool LicenseStatus { get; set; } = true;

        public static explicit operator AppLicense(AppLicenseRequest appLicensereq)
        {
            return new AppLicense 
            {
                AppName = appLicensereq.AppName,
                AppService = appLicensereq.AppService,
                SpaceId = appLicensereq.SpaceId,
                TestStationPool = appLicensereq.TestStationPool,
                UserName = appLicensereq.UserName,
                Password = appLicensereq.Password,
                LastModified = DateTime.UtcNow,
                LicenseStatus = appLicensereq.LicenseStatus
            };
        }

    }
}
