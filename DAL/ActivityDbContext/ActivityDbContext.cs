using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.ActivityDbContext;

public class ActivityDbContext : DbContext
{
	public DbSet<Activity> Activities { get; set; } = null!;
	public DbSet<ActivityContent> ActivityContents { get; set; } = null!;

	public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Activity>(entity =>
		{
			entity.ToTable("Activities");

			entity.HasKey(e => e.Id);

			entity.Property(e => e.Id)
				  .HasColumnName("Id")
				  .HasColumnType("char(36)")
				  .IsRequired();

			entity.Property(e => e.Title)
				  .HasColumnName("Title")
				  .HasColumnType("varchar(255)")
				  .IsRequired();

			entity.Property(e => e.IsPrivate)
				  .HasColumnName("IsPrivate")
				  .HasColumnType("tinyint(1)")
				  .IsRequired();
		});

		modelBuilder.Entity<ActivityContent>(entity =>
		{
			entity.ToTable("ActivityContents");

			entity.HasKey(e => e.Id);

			entity.Property(e => e.Id)
				  .HasColumnName("Id")
				  .HasColumnType("char(36)")
				  .IsRequired();

			entity.Property(e => e.ActivityId)
				  .HasColumnName("ActivityId")
				  .HasColumnType("char(36)")
				  .IsRequired();

			entity.Property(e => e.ContentId)
				  .HasColumnName("ContentId")
				  .HasColumnType("char(36)")
				  .IsRequired();

			entity.HasOne<Activity>()
				  .WithMany(a => a.ActivityContents)
				  .HasForeignKey(ac => ac.ActivityId)
				  .OnDelete(DeleteBehavior.Cascade);
		});
	}
}
