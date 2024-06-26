using Microsoft.EntityFrameworkCore;
using UrbanInsights.Api.Models;

namespace UrbanInsights.Api.Data
{
    public class UrbanInsightsDbContext : DbContext
    {
        public UrbanInsightsDbContext(DbContextOptions<UrbanInsightsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
