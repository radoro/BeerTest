using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeerAPI.Models;

public partial class SqldbFinancePocContext : DbContext
{
    public SqldbFinancePocContext()
    {
    }

    public SqldbFinancePocContext(DbContextOptions<SqldbFinancePocContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Biere> Bieres { get; set; }

    public virtual DbSet<Brasserie> Brasseries { get; set; }


    public virtual DbSet<Grossiste> Grossistes { get; set; }

    public virtual DbSet<GrossisteBiere> GrossisteBieres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnexionStringWorkerLogTest"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Biere>(entity =>
        {
            entity.HasKey(e => e.IdBiere).HasName("PK__Biere__E2331407A30677AC");

            entity.ToTable("Biere", "BackGroundDashboard");

            entity.Property(e => e.Nom).HasMaxLength(50);

            entity.HasOne(d => d.IdBrasserieNavigation).WithMany(p => p.Bieres)
                .HasForeignKey(d => d.IdBrasserie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackGroundDashboard_Brasserie");
        });

        modelBuilder.Entity<Brasserie>(entity =>
        {
            entity.HasKey(e => e.IdBrasserie).HasName("PK__Brasseri__6C0C9BE03150EE54");

            entity.ToTable("Brasserie", "BackGroundDashboard");

            entity.Property(e => e.Nom).HasMaxLength(50);
        });      

        modelBuilder.Entity<Grossiste>(entity =>
        {
            entity.HasKey(e => e.IdGrossiste).HasName("PK__Grossist__8BEFBF1D4CEFD950");

            entity.ToTable("Grossiste", "BackGroundDashboard");

            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<GrossisteBiere>(entity =>
        {
            entity.HasKey(e => new { e.IdGrossiste, e.IdBiere }).HasName("PK_BackGroundDashboard_GrossisteBiere");

            entity.ToTable("GrossisteBiere", "BackGroundDashboard");

            entity.HasOne(d => d.IdBiereNavigation).WithMany(p => p.GrossisteBieres)
                .HasForeignKey(d => d.IdBiere)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackGroundDashboard_GrossisteBiere_Biere");

            entity.HasOne(d => d.IdGrossisteNavigation).WithMany(p => p.GrossisteBieres)
                .HasForeignKey(d => d.IdGrossiste)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackGroundDashboard_GrossisteBiere_Grossiste");
        });      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
