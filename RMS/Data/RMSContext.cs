using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RMS.Entities.Scaffold;

#nullable disable

namespace RMS.Data
{
    public partial class RMSContext : DbContext
    {
        public RMSContext()
        {
        }

        public RMSContext(DbContextOptions<RMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<ProductionMaterial> ProductionMaterials { get; set; }
        public virtual DbSet<RelatedProduct> RelatedProducts { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Name=ConnectionStrings:Mysql");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.Type).HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_address_id_address");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastModified).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventories_products");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("fk_inventories_contacts");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.MetricType).HasDefaultValueSql("'1'");

                entity.Property(e => e.Type).HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_id");
            });

            modelBuilder.Entity<ProductionMaterial>(entity =>
            {
                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ProductionMaterials)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_production_materials_inventory_id");

                entity.HasOne(d => d.Production)
                    .WithMany(p => p.ProductionMaterials)
                    .HasForeignKey(d => d.ProductionId)
                    .HasConstraintName("fk_production_materials_production_id");
            });

            modelBuilder.Entity<RelatedProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.RelatedProductId })
                    .HasName("PRIMARY");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RelatedProductProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_related_products_product_id");

                entity.HasOne(d => d.RelatedProductNavigation)
                    .WithMany(p => p.RelatedProductRelatedProductNavigations)
                    .HasForeignKey(d => d.RelatedProductId)
                    .HasConstraintName("fk_related_products_related_product_id");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Transports)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_transport_address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
