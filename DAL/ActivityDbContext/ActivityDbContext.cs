using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.ActivityDbContext
{
	public class ActivityDbContext : DbContext
	{
			public DbSet<Activity> Activities { get; set; }
			public DbSet<ActivityContent> ActivityContents { get; set; }

			public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
			{
			}

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<Activity>(entity =>
				{
					entity.HasKey(e => e.Id);
					entity.Property(e => e.Title)
						  .IsRequired();
					entity.Property(e => e.IsPrivate)
						  .IsRequired();
				});

				modelBuilder.Entity<ActivityContent>(entity =>
				{
					entity.HasKey(e => e.Id);
					entity.Property(e => e.ActivityId)
						  .IsRequired();
					entity.Property(e => e.ContentId)
						  .IsRequired();

					entity.HasOne<Activity>()
						  .WithMany(a => a.ActivityContents)
						  .HasForeignKey(ac => ac.ActivityId)
						  .OnDelete(DeleteBehavior.Cascade);
				});
			}
	}
}
