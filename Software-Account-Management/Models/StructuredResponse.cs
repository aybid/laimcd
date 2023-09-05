namespace Software_Account_Management.Models
{
    public class StructuredResponse
    {
        public string ApplicationName { get; set; } = null!;

        public string? ApplicationVersion { get; set; }

        public virtual ICollection<LicenseResponse> ReservedLicenses { get; set; } = new List<LicenseResponse>();
        public virtual ICollection<LicenseResponse> UnreservedLicenses { get; set; } = new List<LicenseResponse>();
    }
}
