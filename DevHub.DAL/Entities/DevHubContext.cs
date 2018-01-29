using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevHub.DAL.Entities
{
    public partial class DevHubContext : DbContext
    {
        public virtual DbSet<BookLog> BookLog { get; set; }
        public virtual DbSet<ClientMaster> ClientMaster { get; set; }
        public virtual DbSet<InvAddProducts> InvAddProducts { get; set; }
        public virtual DbSet<InvProductCategories> InvProductCategories { get; set; }
        public virtual DbSet<InvProducts> InvProducts { get; set; }
        public virtual DbSet<InvUnitOfMeasure> InvUnitOfMeasure { get; set; }
        public virtual DbSet<TimeTrackingLogger> TimeTrackingLogger { get; set; }

        public DevHubContext(DbContextOptions<DevHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLog>(entity =>
            {
                entity.Property(e => e.BookingRefCode).HasColumnType("varchar(15)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DateOfArrival).HasColumnType("datetime");

                entity.Property(e => e.DateOfDeparture).HasColumnType("datetime");

                entity.Property(e => e.RoomType).HasMaxLength(50);
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PK_ClientMaster");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ContactNumber1)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ContactNumber2).HasColumnType("varchar(15)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(40)");

                entity.Property(e => e.LastName).HasColumnType("varchar(40)");

                entity.Property(e => e.MiddleName).HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<InvAddProducts>(entity =>
            {
                entity.HasKey(e => e.RecId)
                    .HasName("PK_inv_AddProducts");

                entity.ToTable("inv_AddProducts");

                entity.Property(e => e.DateTimeAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal");
            });

            modelBuilder.Entity<InvProductCategories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_inv_ProductCategories");

                entity.ToTable("inv_ProductCategories");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryDesc)
                    .IsRequired()
                    .HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<InvProducts>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Products");

                entity.ToTable("inv_Products");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription).HasColumnType("varchar(150)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Srp)
                    .HasColumnName("SRP")
                    .HasColumnType("decimal");

                entity.Property(e => e.UomId).HasColumnName("uom_Id");
            });

            modelBuilder.Entity<InvUnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.UomId)
                    .HasName("PK_inv_UnitOfMeasure");

                entity.ToTable("inv_UnitOfMeasure");

                entity.Property(e => e.UomId).HasColumnName("uom_Id");

                entity.Property(e => e.UomDesc)
                    .IsRequired()
                    .HasColumnName("uom_Desc")
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<TimeTrackingLogger>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK_TimeTrackingLogger");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.LoggedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Remarks).HasColumnType("varchar(2000)");

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
            });
        }
    }
}