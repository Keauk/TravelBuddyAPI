using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Data
{
    public class TravelBuddyContext : DbContext
    {
        public TravelBuddyContext(DbContextOptions<TravelBuddyContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripLog> TripLogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}