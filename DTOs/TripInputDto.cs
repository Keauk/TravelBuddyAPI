using System.ComponentModel.DataAnnotations;

namespace TravelBuddyAPI.DTOs
{
    public class TripInputDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Description must be between 10 and 1000 characters")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ?StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ?EndDate { get; set; }
    }
}