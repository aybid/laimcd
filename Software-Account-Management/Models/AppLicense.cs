using System.ComponentModel.DataAnnotations.Schema;

namespace Software_Account_Management.Models
{
    public class AppLicense
    {
        public Guid Id { get; set; }
        public required string AppName { get; set; }
        public required string AppService { get; set; }
        public int SpaceId { get; set; }
        public required string TestStationPool { get; set; }
        public int? LicenseOrderBookId { get; set; }

        [ForeignKey("LicenseOrderBookId")]
        public LicenseOrderBook? Reservation { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public DateTime LastModified { get; set; }
        public bool LicenseStatus { get; set; } = true;

    }
}
