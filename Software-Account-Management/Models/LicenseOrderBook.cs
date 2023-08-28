using System.ComponentModel.DataAnnotations.Schema;

namespace Software_Account_Management.Models
{ 
    public class LicenseOrderBook
    {
        public int Id { get; set; }
        public required string TestStationName { get; set; }
        public required string TestCaseID { get; set; }
        public required string Orchestrator { get; set; }
        public Guid AppLicenseId { get; set; }
 
        [ForeignKey("AppLicenseId")]
        public required AppLicense AppLicense { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime EstCompletionTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public required string ReservedByUser { get; set; }
        public required string Framework { get; set; }
        
    }
}
