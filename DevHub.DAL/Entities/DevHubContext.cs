using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevHub.DAL.Entities
{
    public partial class DevHubContext : DbContext
    {
        public virtual DbSet<BillingTransaction> BillingTransaction { get; set; }
        public virtual DbSet<BookLog> BookLog { get; set; }
        public virtual DbSet<ClientMaster> ClientMaster { get; set; }
        public virtual DbSet<InvAddProducts> InvAddProducts { get; set; }
        public virtual DbSet<InvProductCategories> InvProductCategories { get; set; }
        public virtual DbSet<InvProducts> InvProducts { get; set; }
        public virtual DbSet<InvTransactionOthers> InvTransactionOthers { get; set; }
        public virtual DbSet<InvUnitOfMeasure> InvUnitOfMeasure { get; set; }
        public virtual DbSet<TimeTrackingLogger> TimeTrackingLogger { get; set; }

        public DevHubContext(DbContextOptions<DevHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingTransaction>(entity =>
            {
                entity.HasKey(e => e.BillingId);

                entity.Property(e => e.BillingId)
                    .HasColumnName("BillingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AmountPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.BillingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OtherTransactionAmount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<BookLog>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.BookingRefCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DateOfArrival).HasColumnType("datetime");

                entity.Property(e => e.DateOfDeparture).HasColumnType("datetime");

                entity.Property(e => e.RoomType).HasMaxLength(50);
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ContactNumber1)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Profession)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvAddProducts>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("inv_AddProducts");

                entity.Property(e => e.AddedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<InvProductCategories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("inv_ProductCategories");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryDesc)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvProducts>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("inv_Products");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Srp).HasColumnName("SRP");

                entity.Property(e => e.UomId).HasColumnName("uom_Id");
            });

            modelBuilder.Entity<InvTransactionOthers>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("inv_TransactionOthers");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<InvUnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.UomId);

                entity.ToTable("inv_UnitOfMeasure");

                entity.Property(e => e.UomId)
                    .HasColumnName("uom_Id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UomDesc)
                    .IsRequired()
                    .HasColumnName("uom_Desc")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeTrackingLogger>(entity =>
            {
                entity.HasKey(e => e.TimeTrackerId);

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.LoggedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
            });
        }
    }
}
