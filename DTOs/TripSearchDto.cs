using System.ComponentModel.DataAnnotations;

namespace TravelBuddyAPI.DTOs
{
    public class TripSearchDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Title must be less than 256 characters.")]
        public string Title { get; set; }
    }
}
