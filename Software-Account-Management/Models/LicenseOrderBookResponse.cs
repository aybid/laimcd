namespace Software_Account_Management.Models
{
    public class LicenseOrderBookResponse
    {
        public class AppLicenseDTO 
        {
            public Guid Id { get; set; }
            public required string AppName { get; set; }
            public required string AppService { get; set; }
            public int SpaceId { get; set; }
            public required string TestStationPool { get; set; }
            public required string UserName { get; set; }
            public required string Password { get; set; }
            public DateTime LastModified { get; set; }
            public bool LicenseStatus { get; set; }

        }
        public int Id { get; set; }
        public required string TestStationName { get; set; }
        public required string TestCaseID { get; set; }
        public required string Orchestrator { get; set; }
        public required AppLicenseDTO AppLicense { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime EstCompletionTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public required string ReservedByUser { get; set; }
        public required string Framework { get; set; }

        public static explicit operator LicenseOrderBookResponse(LicenseOrderBook licenseorderbook)
        {
            return new LicenseOrderBookResponse
            {
                Id = licenseorderbook.Id,
                TestStationName = licenseorderbook.TestStationName,
                TestCaseID = licenseorderbook.TestCaseID,
                Orchestrator = licenseorderbook.Orchestrator,
                AppLicense = new AppLicenseDTO
                {
                    Id = licenseorderbook.AppLicense.Id,
                    AppName = licenseorderbook.AppLicense.AppName,
                    AppService = licenseorderbook.AppLicense.AppService,
                    SpaceId = licenseorderbook.AppLicense.SpaceId,
                    TestStationPool = licenseorderbook.AppLicense.TestStationPool,
                    UserName = licenseorderbook.AppLicense.UserName,
                    Password = licenseorderbook.AppLicense.Password,
                    LastModified = licenseorderbook.AppLicense.LastModified,
                    LicenseStatus = licenseorderbook.AppLicense.LicenseStatus
                },
                ReservationTime = licenseorderbook.ReservationTime,
                EstCompletionTime = licenseorderbook.EstCompletionTime,
                CompletionTime = licenseorderbook.CompletionTime,
                ReservedByUser = licenseorderbook.ReservedByUser,
                Framework = licenseorderbook.Framework

            };
        }

    }
}
