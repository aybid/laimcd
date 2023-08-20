namespace Software_Account_Management.Models
{
    public class AppLicenseDTO
    { 
        public required string AppName { get; set; }
        public required string AppService { get; set; }
        public int SpaceId { get; set; }
        public required string TestStationPool { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public DateTime LastModified { get; set; }
        public bool LicenseStatus { get; set; } = true;

    }
}
