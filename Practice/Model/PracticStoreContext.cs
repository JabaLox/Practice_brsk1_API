using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practice.Model;

public partial class PracticStoreContext : DbContext
{
    public static PracticStoreContext _context { get; } = new PracticStoreContext();
    public PracticStoreContext()
    {
    }

    public PracticStoreContext(DbContextOptions<PracticStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacture> Manufactures { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PracticStore;Username=postgres;Password=kolyan28");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).ValueGeneratedNever();
        });

        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.HasKey(e => e.IdManufacture).HasName("Manufacture_pkey");

            entity.ToTable("Manufacture");

            entity.Property(e => e.IdManufacture).ValueGeneratedNever();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.IdOrder).ValueGeneratedNever();

            entity.HasOne(d => d.UserOrderNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.ArticleProduct, e.IdOrder }).HasName("OrderProduct_pkey");

            entity.ToTable("OrderProduct");

            entity.HasOne(d => d.ArticleProductNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ArticleProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleProduct).HasName("Product_pkey");

            entity.ToTable("Product");

            entity.Property(e => e.CostProduct).HasColumnType("money");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category");

            entity.HasOne(d => d.ManufactureNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("manufacture");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.IdUser).ValueGeneratedNever();

            entity.HasOne(d => d.RoleUserNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
