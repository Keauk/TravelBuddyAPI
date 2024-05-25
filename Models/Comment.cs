using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TravelBuddyAPI.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [Required]
        [ForeignKey("TripLog")]
        public int TripLogId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CommentDate { get; set; }

        public int Likes { get; set; }

        public Comment()
        {
            CommentDate = DateTime.UtcNow;
        }
    }
}