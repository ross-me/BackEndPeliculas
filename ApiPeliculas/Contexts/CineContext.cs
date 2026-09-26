using System;
using System.Collections.Generic;
using ApiCine.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCine.Contexts;

public partial class CineContext : DbContext
{
    public CineContext()
    {
    }

    public CineContext(DbContextOptions<CineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<pelicula> peliculas { get; set; }

    public virtual DbSet<pelicula_salacine> pelicula_salacines { get; set; }

    public virtual DbSet<sala_cine> sala_cines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=cinedb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<pelicula>(entity =>
        {
            entity.HasKey(e => e.id_pelicula).HasName("PK__pelicula__B5017F4D4AD890E4");

            entity.ToTable("pelicula");

            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<pelicula_salacine>(entity =>
        {
            entity.HasKey(e => e.id_pelicula_sala).HasName("PK__pelicula__39BC477F7665FA58");

            entity.ToTable("pelicula_salacine");

            entity.HasOne(d => d.id_peliculaNavigation).WithMany(p => p.pelicula_salacines)
                .HasForeignKey(d => d.id_pelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pelisala_peli");

            entity.HasOne(d => d.id_sala_cineNavigation).WithMany(p => p.pelicula_salacines)
                .HasForeignKey(d => d.id_sala_cine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pelisala_sala");
        });

        modelBuilder.Entity<sala_cine>(entity =>
        {
            entity.HasKey(e => e.id_sala).HasName("PK__sala_cin__D18B015B5AAA2DC7");

            entity.ToTable("sala_cine");

            entity.Property(e => e.estado).HasDefaultValue(true);
            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
