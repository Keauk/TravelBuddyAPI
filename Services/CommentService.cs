using TravelBuddyAPI.Models;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TravelBuddyAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly TravelBuddyContext _context;

        public CommentService(TravelBuddyContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForTripLogAsync(int tripLogId)
        {
            return await _context.Comments
                                 .Where(c => c.TripLogId == tripLogId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForUserAsync(int userId)
        {
            return await _context.Comments
                                 .Where(c => c.UserId == userId)
                                 .ToListAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
