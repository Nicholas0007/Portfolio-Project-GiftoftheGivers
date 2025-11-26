using System;

namespace DisasterAlleviationFoundation.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime DateDonated { get; set; } = DateTime.Now;
    }
}
