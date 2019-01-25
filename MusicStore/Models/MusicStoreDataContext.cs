using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class MusicStoreDataContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Artist>().ToTable("Artist");

            modelBuilder.Entity<Artist>().Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ArtistDetail>().ToTable("ArtistDetail");

            modelBuilder.Entity<ArtistDetail>()
                .HasKey(a => a.ArtistID);

            modelBuilder.Entity<Artist>()
                .HasOptional(a => a.ArtistDetail)
                .WithRequired(a => a.Artist);


            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Album>().Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Album>().Property(a => a.AlbumID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Reviewer>().Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

          
            base.OnModelCreating(modelBuilder);

        }
    }
}