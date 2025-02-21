using DramaDayScraper.Table.Cell.Episodes.Entities;
using DramaDayScraper.Table.Cell.EpisodeVersion;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.MediaVersions;
using DramaDayScraper.Table.Cell.Seasons;
using Microsoft.EntityFrameworkCore;

namespace DramaDayScraper.DALtest
{
    internal class DramaDayContext : DbContext
    {
        public DramaDayContext() { }

        public DramaDayContext(DbContextOptions<DramaDayContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=HallyuTest;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Media> Media { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<MediaVersion> MediaVersions { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<ShortLink> ShortLinks { get; set; }

        // Episode Types
        public DbSet<SingleEpisode> SingleEpisodes { get; set; }
        public DbSet<SpecialEpisode> SpecialEpisodes { get; set; }
        public DbSet<UknownEpisode> UknownEpisodes { get; set; }
        public DbSet<BatchEpisode> BatchEpisodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Media
            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(m => m.DramaDayId);
                entity.Property(m => m.KrTitle).IsRequired(false);
                entity.Property(m => m.EnTitle).IsRequired();

                entity.HasMany(m => m.Seasons)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure DramaDayId to be generated on add
                entity.Property(m => m.DramaDayId)
                      .ValueGeneratedOnAdd(); // auto-increment
            });

            // Seasons
            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasMany(s => s.MediaVersions)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure Id to be generated on add
                entity.Property(s => s.Id)
                      .ValueGeneratedOnAdd(); // auto-increment
            });

            // MediaVersion
            modelBuilder.Entity<MediaVersion>(entity =>
            {
                entity.HasKey(mv => mv.Id);
                entity.HasMany(mv => mv.Episodes)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure Id to be generated on add
                entity.Property(mv => mv.Id)
                      .ValueGeneratedOnAdd(); // auto-increment
            });

            // Episode (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Episode>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.EpisodeVersions)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure Id to be generated on add
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // auto-increment
            });

            modelBuilder.Entity<SingleEpisode>().ToTable("SingleEpisodes");
            modelBuilder.Entity<SpecialEpisode>().ToTable("SpecialEpisodes");
            modelBuilder.Entity<UknownEpisode>().ToTable("UknownEpisodes");
            modelBuilder.Entity<BatchEpisode>().ToTable("BatchEpisodes");

            // EpVersion
            modelBuilder.Entity<EpVersion>(entity =>
            {
                entity.HasKey(ev => ev.Id);
                entity.HasMany(ev => ev.Links)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure Id to be generated on add
                entity.Property(ev => ev.Id)
                      .ValueGeneratedOnAdd(); // auto-increment
            });

            // ShortLink
            modelBuilder.Entity<ShortLink>(entity =>
            {
                entity.HasKey(sl => sl.Id);
                entity.Property(sl => sl.Host).IsRequired();
                entity.Property(sl => sl.LinkUrl).IsRequired();

                // Configure Id to be generated on add
                entity.Property(sl => sl.Id)
                      .ValueGeneratedOnAdd(); // auto-increment
            });
        }
    }
}
