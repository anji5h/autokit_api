using AutoKitApi.Models;

namespace AutoKitApi.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Bag> Bags { get; set; }
    public DbSet<BagItem> BagItems { get; set; }
    public DbSet<Operation> Operations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
        });

        modelBuilder.Entity<Bag>(entity =>
        {
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
        });

        modelBuilder.Entity<BagItem>(entity =>
        {
            entity.HasOne<Product>(bi => bi.Product)
                .WithMany(p => p.BagItems)
                .HasForeignKey(bi => bi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(e => e.PackagedAt).HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
            entity.HasOne<Bag>(b => b.Bag)
                .WithMany(b => b.Operations)
                .HasForeignKey(o => o.BagId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Product>(p => p.Product)
                .WithMany(p => p.Operations)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}