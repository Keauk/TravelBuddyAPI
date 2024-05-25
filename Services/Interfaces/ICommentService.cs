using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsForUserAsync(int userId);
        Task<IEnumerable<Comment>> GetCommentsForTripLogAsync(int tripLogId);
    }
}
