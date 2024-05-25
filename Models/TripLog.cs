using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelBuddyAPI.Models
{
    public class TripLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripLogId { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public string PhotoPath { get; set; }
    }
}