using EduToyRent.DAL.Entities;
using EduToyRent.DataAccess.Context.Configuration;
using EduToyRent.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace EduToyRent.DAL.Context
{
    public class EduToyRentDbContext : DbContext
    {
        public EduToyRentDbContext(DbContextOptions<EduToyRentDbContext> options)
        : base(options)
        {
        }

        #region DBset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountVoucher> AccountVouchers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<DepositOrder> DepositOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
		public DbSet<ResetPasswordOTP> ResetPasswordOTPs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShipDate> ShipDates { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<RequestForm> RequestForms { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<StatusOrder> StatusOrders {  get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Toys)
                .WithOne(t => t.Supplier)
                .HasForeignKey(t => t.SupplierId);

            modelBuilder.Entity<Account>()
    .HasMany(a => a.Carts)
    .WithOne(c => c.Account)
    .HasForeignKey(c => c.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Orders)
                .WithOne(o => o.Account)
                .HasForeignKey(o => o.AccountId);

            modelBuilder.Entity<Review>()
        .HasOne(r => r.Account)
        .WithMany(a => a.Reviews)
        .HasForeignKey(r => r.AccountId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountVouchers)
                .WithOne(av => av.Account)
                .HasForeignKey(av => av.AccountId);

            modelBuilder.Entity<Payment>()
        .HasOne(p => p.Account)
        .WithMany(a => a.Payments)
        .HasForeignKey(p => p.AccountId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.RefreshTokens)
                .WithOne(rt => rt.Account)
                .HasForeignKey(rt => rt.AccountId);


			modelBuilder.Entity<ResetPasswordOTP>()
			.HasKey(otp => otp.id);

			modelBuilder.Entity<OrderDetail>()
        .HasOne(od => od.Order)
        .WithMany(o => o.OrderDetails)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepositOrder>()
                .HasOne(de => de.Order)
                .WithOne(o => o.DepositOrders)
                .HasForeignKey<DepositOrder>(de => de.OrderId);


            modelBuilder.Entity<Payment>()
        .HasOne(p => p.Order)
        .WithMany(o => o.Payments)
        .HasForeignKey(p => p.OrderId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.ShipDates)
                .WithOne(sd => sd.OrderDetail)
                .HasForeignKey<ShipDate>(sd => sd.OrderDetailId);

            modelBuilder.Entity<Review>()
        .HasOne(r => r.Toy)
        .WithMany(t => t.Reviews)
        .HasForeignKey(r => r.ToyId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
         .HasOne(od => od.Toy)
         .WithMany(t => t.OrderDetails)
         .HasForeignKey(od => od.ToyId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
        .HasOne(ci => ci.Toy)
        .WithMany(t => t.CartItems)
        .HasForeignKey(ci => ci.ToyId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
        .HasOne(ci => ci.Cart)
        .WithMany(c => c.CartItems)
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voucher>()
                .HasMany(v => v.AccountVouchers)
                .WithOne(av => av.Voucher)
                .HasForeignKey(av => av.VoucherId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);

            modelBuilder.Entity<Toy>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Toys)
                .HasForeignKey(t => t.CategoryId);
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<DepositOrder>()
                .Property(d => d.RefundMoney)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DepositOrder>()
                .Property(d => d.TotalMoney)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.FinalMoney)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalMoney)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.RentalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Toy>()
                .Property(t => t.BuyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Toy>()
                .Property(t => t.RentPricePerDay)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Toy>()
                .Property(t => t.RentPricePerTwoWeeks)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Toy>()
                .Property(t => t.RentPricePerWeek)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RequestForm>()
    .HasOne(r => r.Toy)
    .WithMany(t => t.RequestForms)
    .HasForeignKey(r => r.ToyId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestForm>()
    .HasOne(r => r.Account)
    .WithMany(a => a.RequestForms)
    .HasForeignKey(r => r.ProcessedById)
    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Report>()
        .HasOne(r => r.Toy)
        .WithMany(t => t.Reports)
        .HasForeignKey(r => r.ToyId)
        .OnDelete(DeleteBehavior.Restrict); // Hoặc DeleteBehavior.NoAction

            modelBuilder.Entity<Report>()
       .HasOne(r => r.Toy)
       .WithMany(t => t.Reports)
       .HasForeignKey(r => r.ToyId)
       .OnDelete(DeleteBehavior.Restrict); // Hoặc DeleteBehavior.NoAction


            modelBuilder.Entity<Report>()
                .HasOne(r => r.Account)
                .WithMany(a => a.Reports)
                .HasForeignKey(r => r.ReportById)
                .OnDelete(DeleteBehavior.Restrict); // Hoặc DeleteBehavior.NoAction

            modelBuilder.Entity<StatusOrder>()
            .HasMany(r => r.Orders)
            .WithOne(a => a.StatusOrder)
            .HasForeignKey(a => a.StatusId);

            //Add-Migration InitMigration -Context EduToyRentDbContext -Project EduToyRent.DataAccess -StartupProject EduToyRent.API -OutputDir Context/Migrations


        }
    }
}
