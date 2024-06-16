using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Persistence;

public class StreamerDbContext : DbContext
{
    public DbSet<Streamer>? Streaners { get; set; }
    public DbSet<Video>? Videos { get; set; }
    public DbSet<Actor>? Actores { get; set; }
    public DbSet<Director>? Directores { get; set; }

    public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "system";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "system";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(o => o.Streamer)
            .HasForeignKey(fk => fk.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(m => m.Actores)
            .WithMany(m => m.Videos)
            .UsingEntity<VideoActor>(pk => pk.HasKey(e => new { e.VideoId, e.ActorId }));
    }
}