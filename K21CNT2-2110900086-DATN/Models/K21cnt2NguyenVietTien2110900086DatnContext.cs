using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace K21CNT2_2110900086_DATN.Models;

public partial class K21cnt2NguyenVietTien2110900086DatnContext : DbContext
{
    public K21cnt2NguyenVietTien2110900086DatnContext()
    {
    }

    public K21cnt2NguyenVietTien2110900086DatnContext(DbContextOptions<K21cnt2NguyenVietTien2110900086DatnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TIEENS\\TIEN;Database=K21CNT2_NguyenVietTien_2110900086_DATN;Integrated Security=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__B19E45C9F6E25792");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__Role_ID__45F365D3");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__C164D018C12867BA");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("Blog_ID");
            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.BlogName).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB38D4EE7677463");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("PK__Color__795F1D749E09AE76");

            entity.ToTable("Color");

            entity.Property(e => e.ColorId).HasColumnName("Color_ID");
            entity.Property(e => e.ColorName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8CB286B9D07273A5");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(11);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__F1E4639BF69628E4");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Order__Customer___4CA06362");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Order__Status_ID__4D94879B");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__53D880E0969F3F8E");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetail_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5070F446");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__5165187F");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__DA6C7FE1F844FF5B");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payment__Custome__5535A963");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__Order_I__5441852A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9834FB9A1418B6BC");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AgeRange).HasMaxLength(50);
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.ColorId).HasColumnName("Color_ID");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Material).HasMaxLength(100);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.SizeId).HasColumnName("Size_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__3D5E1FD2");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK__Product__Color_I__3F466844");

            entity.HasOne(d => d.Size).WithMany(p => p.Products)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__Product__Size_ID__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80AB49BD057DFAD");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__0BC32540B638AE50");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("Size_ID");
            entity.Property(e => e.SizeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__519009ACE2B3F40A");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
