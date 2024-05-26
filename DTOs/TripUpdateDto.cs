using System.ComponentModel.DataAnnotations;

namespace TravelBuddyAPI.DTOs
{
    public class TripUpdateDto : TripInputDto
    {
        public int UserId { get; set; }
    }
}