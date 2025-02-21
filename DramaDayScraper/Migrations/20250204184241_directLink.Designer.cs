﻿// <auto-generated />
using DramaDayScraper.DALtest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DramaDayScraper.Migrations
{
    [DbContext(typeof(DramaDayContext))]
    [Migration("20250204184241_directLink")]
    partial class directLink
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DramaDayScraper.Media", b =>
                {
                    b.Property<string>("DramaDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EnTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KrTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DramaDayId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.LinkVersion.EpVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<string>("EpisodeVerisonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.ToTable("EpVersion");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.Row", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MediaVersionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MediaVersionId");

                    b.ToTable("Episodes");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.LinksGroup.ShortLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EpVersionId")
                        .HasColumnType("int");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EpVersionId");

                    b.ToTable("ShortLinks");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.MediaVersions.MediaVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MediaVersionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("MediaVersions");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Seasons.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MediaDramaDayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SeasonNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MediaDramaDayId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.BatchEpisode", b =>
                {
                    b.HasBaseType("DramaDayScraper.Table.Cell.Episodes.Entities.Row");

                    b.Property<int>("RangeEnd")
                        .HasColumnType("int");

                    b.Property<int>("RangeStart")
                        .HasColumnType("int");

                    b.ToTable("BatchEpisodes", (string)null);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.SingleEpisode", b =>
                {
                    b.HasBaseType("DramaDayScraper.Table.Cell.Episodes.Entities.Row");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.ToTable("SingleEpisodes", (string)null);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.SpecialEpisode", b =>
                {
                    b.HasBaseType("DramaDayScraper.Table.Cell.Episodes.Entities.Row");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SpecialEpisodes", (string)null);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.UknownEpisode", b =>
                {
                    b.HasBaseType("DramaDayScraper.Table.Cell.Episodes.Entities.Row");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UknownEpisodes", (string)null);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.LinkVersion.EpVersion", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Episodes.Entities.Row", null)
                        .WithMany("EpisodeVersions")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.Row", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.MediaVersions.MediaVersion", null)
                        .WithMany("Episodes")
                        .HasForeignKey("MediaVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.LinksGroup.ShortLink", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.LinkVersion.EpVersion", null)
                        .WithMany("Links")
                        .HasForeignKey("EpVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.MediaVersions.MediaVersion", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Seasons.Season", null)
                        .WithMany("MediaVersions")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Seasons.Season", b =>
                {
                    b.HasOne("DramaDayScraper.Media", null)
                        .WithMany("Seasons")
                        .HasForeignKey("MediaDramaDayId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.BatchEpisode", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Episodes.Entities.Row", null)
                        .WithOne()
                        .HasForeignKey("DramaDayScraper.Table.Cell.Episodes.Entities.BatchEpisode", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.SingleEpisode", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Episodes.Entities.Row", null)
                        .WithOne()
                        .HasForeignKey("DramaDayScraper.Table.Cell.Episodes.Entities.SingleEpisode", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.SpecialEpisode", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Episodes.Entities.Row", null)
                        .WithOne()
                        .HasForeignKey("DramaDayScraper.Table.Cell.Episodes.Entities.SpecialEpisode", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.UknownEpisode", b =>
                {
                    b.HasOne("DramaDayScraper.Table.Cell.Episodes.Entities.Row", null)
                        .WithOne()
                        .HasForeignKey("DramaDayScraper.Table.Cell.Episodes.Entities.UknownEpisode", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DramaDayScraper.Media", b =>
                {
                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.LinkVersion.EpVersion", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Episodes.Entities.Row", b =>
                {
                    b.Navigation("EpisodeVersions");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.MediaVersions.MediaVersion", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("DramaDayScraper.Table.Cell.Seasons.Season", b =>
                {
                    b.Navigation("MediaVersions");
                });
#pragma warning restore 612, 618
        }
    }
}
