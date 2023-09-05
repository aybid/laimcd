namespace Software_Account_Management.Models;

public partial class License
{
    public Guid LicenseId { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid LicenseVendorId { get; set; }

    public int SpaceId { get; set; }

    public string? TestStationPool { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? ExpiredOn { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual LicenseVendor LicenseVendor { get; set; } = null!;
}
