namespace Software_Account_Management.Models
{
    public class LicenseOrderBook
    {
        public enum STATUS
        {
            UNQUEUED,
            QUEUED,
            RESERVED,
            COMPLETED,
            CANCELLED
        }
        public int Id { get; set; }
        public AppLicense? App_license { get; set; }
        public required string TestStation { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime CompletionTime { get; set; }
        // we need to have an enum defined for this
        public required STATUS OrderStatus { get; set; } = STATUS.UNQUEUED;
        public required string ReservedByUser { get; set; }
        public required string ReservedForSut { get; set; }
        public required string InstanceId { get; set; }
        public required string TestStationTaskId { get; set; }

        
    }
}
