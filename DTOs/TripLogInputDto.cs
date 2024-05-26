using System.ComponentModel.DataAnnotations;

namespace TravelBuddyAPI.DTOs
{
    public class TripLogInputDto
    {
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "PhotoPath is required")]
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date {  get; set; }
    }
}