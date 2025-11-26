using System;

namespace DisasterAlleviationFoundation.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skills { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;
    }
}
