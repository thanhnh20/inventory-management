using System;
using System.IO;
using Library.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Library.Model
{
    public partial class InventoryManagementContext : DbContext
    {
        public InventoryManagementContext()
        {
        }

        public InventoryManagementContext(DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Consignment> Consignments { get; set; }
        public virtual DbSet<ConsignmentDetail> ConsignmentDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InvoiceInput> InvoiceInputs { get; set; }
        public virtual DbSet<InvoiceInputDetail> InvoiceInputDetails { get; set; }
        public virtual DbSet<InvoiceOutput> InvoiceOutputs { get; set; }
        public virtual DbSet<InvoiceOutputDetail> InvoiceOutputDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Suplier> Supliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(MyServices.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Consignment>(entity =>
            {
                entity.ToTable("Consignment");

                entity.Property(e => e.ConsignmentId).HasColumnName("consignmentID");

                entity.Property(e => e.ConsignmentName)
                    .HasMaxLength(50)
                    .HasColumnName("consignmentName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ConsignmentDetail>(entity =>
            {
                entity.ToTable("Consignment_Detail");

                entity.Property(e => e.ConsignmentDetailId).HasColumnName("consignmentDetailID");

                entity.Property(e => e.ConsignmentId).HasColumnName("consignmentID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Consignment)
                    .WithMany(p => p.ConsignmentDetails)
                    .HasForeignKey(d => d.ConsignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSIGNMENT_ID_PRODUCT_CONSIGNMENT");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ConsignmentDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ID_PRODUCT_CONSIGNMENT");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.CustomerAddress).HasColumnName("customerAddress");

                entity.Property(e => e.CustomerName).HasColumnName("customerName");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(11)
                    .HasColumnName("customerPhone");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<InvoiceInput>(entity =>
            {
                entity.HasKey(e => e.InputBillId)
                    .HasName("PK__Invoice___7FFF4C36FD16FF10");

                entity.ToTable("Invoice_Input");

                entity.Property(e => e.InputBillId).HasColumnName("inputBillID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.InputDate)
                    .HasColumnType("date")
                    .HasColumnName("inputDate");

                entity.Property(e => e.SuplierId).HasColumnName("suplierID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Suplier)
                    .WithMany(p => p.InvoiceInputs)
                    .HasForeignKey(d => d.SuplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUPLIER_ID_INVOICE_INPUT");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvoiceInputs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ID_INVOICE_INPUT");
            });

            modelBuilder.Entity<InvoiceInputDetail>(entity =>
            {
                entity.HasKey(e => e.InputDetailId)
                    .HasName("PK__Invoice___357FF83DDE625367");

                entity.ToTable("Invoice_InputDetails");

                entity.Property(e => e.InputDetailId).HasColumnName("inputDetailID");

                entity.Property(e => e.ConsignmentId).HasColumnName("consignmentID");

                entity.Property(e => e.InputBillId).HasColumnName("inputBillID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Consignment)
                    .WithMany(p => p.InvoiceInputDetails)
                    .HasForeignKey(d => d.ConsignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSIGNMENT_ID_INVOICE_INPUTDETAILS");

                entity.HasOne(d => d.InputBill)
                    .WithMany(p => p.InvoiceInputDetails)
                    .HasForeignKey(d => d.InputBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INPUTBILL_ID_Invoice_InputDetails");
            });

            modelBuilder.Entity<InvoiceOutput>(entity =>
            {
                entity.HasKey(e => e.OutputBillId)
                    .HasName("PK__Invoice___D62D78DBC785E469");

                entity.ToTable("Invoice_Output");

                entity.Property(e => e.OutputBillId).HasColumnName("outputBillID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.OutputDate)
                    .HasColumnType("date")
                    .HasColumnName("outputDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InvoiceOutputs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMERID_ID_INVOICE_OUTPUT");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvoiceOutputs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ID_INVOICE_OUTPUT");
            });

            modelBuilder.Entity<InvoiceOutputDetail>(entity =>
            {
                entity.HasKey(e => e.OutputDetailId)
                    .HasName("PK__Invoice___A01DBC981BACEA19");

                entity.ToTable("Invoice_OutputDetails");

                entity.Property(e => e.OutputDetailId).HasColumnName("outputDetailID");

                entity.Property(e => e.ConsignmentId).HasColumnName("consignmentID");

                entity.Property(e => e.OutputBillId).HasColumnName("outputBillID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Consignment)
                    .WithMany(p => p.InvoiceOutputDetails)
                    .HasForeignKey(d => d.ConsignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSIGNMENT_ID_INVOICE_OUTPUTDETAILS");

                entity.HasOne(d => d.OutputBill)
                    .WithMany(p => p.InvoiceOutputDetails)
                    .HasForeignKey(d => d.OutputBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OUTPUTBILL_ID_INVOICE_OUTPUTDETAILS");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.ImportPrice).HasColumnName("importPrice");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.SellingPrice).HasColumnName("sellingPrice");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalQuantity).HasColumnName("totalQuantity");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .HasColumnName("unit");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CATEGORY_ID_PRODUCT");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.ToTable("Suplier");

                entity.HasIndex(e => e.TaxCode, "UQ__Suplier__D97858A68F57E08B")
                    .IsUnique();

                entity.Property(e => e.SuplierId).HasColumnName("suplierID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SuplierName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("suplierName");

                entity.Property(e => e.SuplierPhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("suplierPhone");

                entity.Property(e => e.TaxCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("taxCode");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "UQ__User__F3DBC5721A703B97")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.BirthDay)
                    .HasColumnType("date")
                    .HasColumnName("birthDay");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLEID_USER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
