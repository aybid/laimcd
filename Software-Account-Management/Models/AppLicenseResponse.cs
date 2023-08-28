namespace Software_Account_Management.Models
{
    public class AppLicenseResponse
    {
        public class OrderDTO {
            public int Id { get; set; }
            public required string TestStationName { get; set; }
            public required string TestCaseID { get; set; }
            public required string Orchestrator { get; set; }
            public DateTime ReservationTime { get; set; }
            public DateTime EstCompletionTime { get; set; }
            public DateTime? CompletionTime { get; set; }
            public required string ReservedByUser { get; set; }
            public required string Framework { get; set; }

        }
        public Guid Id { get; set; }
        public required string AppName { get; set; }
        public required string AppService { get; set; }
        public int SpaceId { get; set; }
        public required string TestStationPool { get; set; }
        public OrderDTO? Reservation { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public DateTime LastModified { get; set; }
        public bool LicenseStatus { get; set; }




        public static explicit operator AppLicenseResponse(AppLicense appLicense)
        {
            
            return new AppLicenseResponse
            {
                Id = appLicense.Id,
                AppName = appLicense.AppName,
                AppService = appLicense.AppService,
                SpaceId = appLicense.SpaceId,
                TestStationPool = appLicense.TestStationPool,
                Reservation = appLicense.Reservation != null? new OrderDTO {
                    Id = appLicense.Reservation.Id,
                    TestStationName = appLicense.Reservation.TestStationName,                
                    TestCaseID = appLicense.Reservation.TestCaseID,
                    Orchestrator = appLicense.Reservation.Orchestrator,
                    ReservationTime = appLicense.Reservation.ReservationTime,
                    EstCompletionTime = appLicense.Reservation.EstCompletionTime,
                    CompletionTime = appLicense.Reservation.CompletionTime,
                    ReservedByUser = appLicense.Reservation.ReservedByUser,
                    Framework = appLicense.Reservation.Framework
                    }: null ,
                UserName = appLicense.UserName,
                Password = appLicense.Password,
                LastModified = appLicense.LastModified,
                LicenseStatus = appLicense.LicenseStatus

            };
        }
    }
}
