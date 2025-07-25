using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Stylo_Spin.Models;

public partial class StyloSpinContext : DbContext
{
    public StyloSpinContext()
    {
    }

    public StyloSpinContext(DbContextOptions<StyloSpinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutU> AboutUs { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblContactU> TblContactUs { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSlider> TblSliders { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PRATHAM\\SQLEXPRESS;Database=StyloSpin;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AboutU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AboutUs__3214EC07A117D9D6");

            entity.Property(e => e.Heading).HasMaxLength(255);
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.SubHeading).HasMaxLength(200);
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__tblCateg__A9FDEC3260DAA25C");

            entity.ToTable("tblCategory");

            entity.Property(e => e.CId).HasColumnName("C_Id");
            entity.Property(e => e.CName)
                .HasMaxLength(255)
                .HasColumnName("c_Name");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblContactU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblConta__3214EC07E75BA7C3");

            entity.ToTable("tblContactUs");

            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(100);
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblCusto__3214EC0701839AFA");

            entity.ToTable("tblCustomer");

            entity.HasIndex(e => e.CEmail, "UQ__tblCusto__5D1915F92ED82940").IsUnique();

            entity.Property(e => e.CEmail)
                .HasMaxLength(200)
                .HasColumnName("C_Email");
            entity.Property(e => e.CName)
                .HasMaxLength(200)
                .HasColumnName("C_Name");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.OId).HasName("PK__tblOrder__5AAB0C38F73010B2");

            entity.ToTable("tblOrder");

            entity.Property(e => e.OId).HasColumnName("O_Id");
            entity.Property(e => e.CId).HasColumnName("C_Id");
            entity.Property(e => e.Message).HasMaxLength(200);
            entity.Property(e => e.OrdaerDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ordaerDate");
            entity.Property(e => e.PId).HasColumnName("P_Id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Price");

            entity.HasOne(d => d.CIdNavigation).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.CId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblOrder_tblCustomer");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblOrder_tblProduct");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__tblProdu__82E37F4986C47A31");

            entity.ToTable("tblProduct", tb => tb.HasTrigger("trg_UpdateStatusBasedOnQuantity"));

            entity.Property(e => e.PId).HasColumnName("p_Id");
            entity.Property(e => e.CId).HasColumnName("c_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.PName)
                .HasMaxLength(200)
                .HasColumnName("p_Name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.CIdNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CId)
                .HasConstraintName("FK_tblProduct_tblCategory");
        });

        modelBuilder.Entity<TblSlider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblSlide__3214EC075D7A54D3");

            entity.ToTable("tblSlider");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageName).HasMaxLength(255);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3213E83F5EB91795");

            entity.ToTable("tblUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userEmail");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
