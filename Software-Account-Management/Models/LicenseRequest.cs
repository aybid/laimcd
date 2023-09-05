namespace Software_Account_Management.Models
{
    public class LicenseRequest
    {
        public required String ApplicationName{ get; set; }
        public String? ApplicationVersion { get; set; }
        public required String LicenseVendor { get; set; }
        public required String SpaceName { get; set; }
        public List<String> TestStationPool { get; set; } = new List<String>();
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ExpiredOn { get; set; }
    }
}
