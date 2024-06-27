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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine); // Log to the console
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Location entity
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations"); 
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.Email).HasColumnName("email");
            });

            // Event entity
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name"); // Corrected property name
                entity.Property(e => e.Date).HasColumnName("date");   // Corrected property name
                entity.Property(e => e.LocationId).HasColumnName("location_id"); // Foreign key
            });
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
