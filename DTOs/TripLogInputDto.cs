using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddyAPI.DTOs
{
    public class TripLogInput
    {
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "PhotoPath is required")]
        public string PhotoPath { get; set; }
    }
}