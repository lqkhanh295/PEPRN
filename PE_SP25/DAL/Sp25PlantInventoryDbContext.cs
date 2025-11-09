using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL;

public partial class Sp25PlantInventoryDbContext : DbContext
{
    public Sp25PlantInventoryDbContext()
    {
    }

    public Sp25PlantInventoryDbContext(DbContextOptions<Sp25PlantInventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }


    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3FCAFCC7A");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId)
                .ValueGeneratedNever()
                .HasColumnName("InventoryID");
            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Supplier).HasMaxLength(150);

            entity.HasOne(d => d.Plant).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Inventory__Plant__412EB0B6");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__Plant__98FE46BC517003DC");

            entity.ToTable("Plant");

            entity.Property(e => e.PlantId)
                .ValueGeneratedNever()
                .HasColumnName("PlantID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Origin).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__SystemUs__1788CCAC94759F71");

            entity.ToTable("SystemUser");

            entity.HasIndex(e => e.UserEmail, "UQ__SystemUs__08638DF84F64FD6E").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
