using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class DisasterReport
    {
        // This is the primary key - the unique ID for each report
        public int Id { get; set; }

        [Required] // This data annotation means this field is mandatory
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Urgency { get; set; } // We'll use a dropdown for this later

        // This will automatically be set to the current date when a report is created
        public DateTime DateReported { get; set; } = DateTime.Now;
    }
}
