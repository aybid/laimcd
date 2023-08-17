namespace Software_Account_Management.Models
{
    public class LicenseQueue
    {
        public int Id { get; set; }
        public required LicenseOrderBook Reservation { get; set; }
    }
}
