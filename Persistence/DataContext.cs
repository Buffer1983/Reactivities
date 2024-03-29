using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivityAttendees {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Creating primary key  for table ActivityAttendee
            builder.Entity<ActivityAttendee>( x => x.HasKey(aa => new {aa.AppUserId,aa.ActivityID})); 
            
            //Creating relationship of table with many to many
            builder.Entity<ActivityAttendee>()
                .HasOne(u=>u.AppUser)
                .WithMany(a=> a.Activities)
                .HasForeignKey(aa=> aa.AppUserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(u=>u.Activity)
                .WithMany(a=> a.Attendees)
                .HasForeignKey(aa=> aa.ActivityID);

        }
    }
}