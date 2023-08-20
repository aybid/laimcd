namespace Software_Account_Management.Models
{
    public class LicenseQueueDTO
    {
        public int Id { get; set; }
        public required LicenseOrderBook Reservation { get; set; }
    }
}
