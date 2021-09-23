using Blog.Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Publicacion> Publicaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("Publicacion");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.PublishedAt).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

        }
    }
}
