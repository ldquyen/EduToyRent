using EduToyRent.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.DbContexts
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

        public DbSet<Role> Roles { get; set; }
        public DbSet<ShipDate> ShipDates { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Toys)
                .WithOne(t => t.Supplier)
                .HasForeignKey(t => t.SupplierId);

            modelBuilder.Entity<Account>() 
                .HasOne(a => a.Cart)
                .WithOne(c => c.Account)
                .HasForeignKey<Cart>(c => c.AccountId);

            modelBuilder.Entity<Account>() 
                .HasMany(a => a.Orders)
                .WithOne(o => o.Account)
                .HasForeignKey(o => o.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountVouchers)
                .WithOne(av => av.Account)
                .HasForeignKey(av => av.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Payments)
                .WithOne(p => p.Account)
                .HasForeignKey(p => p.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.RefreshTokens)
                .WithOne(rt => rt.Account)
                .HasForeignKey(rt => rt.AccountId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.DepositOrders)
                .WithOne(de => de.Order)
                .HasForeignKey(de => de.OrderId);

            modelBuilder.Entity<Toy>()
                .HasMany(t => t.Reviews)
                .WithOne(r => r.Toy)
                .HasForeignKey(r => r.ToyId);

            modelBuilder.Entity<Toy>()
                .HasMany(t => t.OrderDetails)
                .WithOne(od => od.Toy)
                .HasForeignKey(od => od.ToyId);

            modelBuilder.Entity<Toy>()
                .HasMany(t => t.CartItems)
                .WithOne(ci => ci.Toy)
                .HasForeignKey(ci => ci.ToyId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Voucher>()
                .HasMany(v => v.AccountVouchers)
                .WithOne(av => av.Voucher)
                .HasForeignKey(av => av.VoucherId);

            modelBuilder.Entity<Role>() 
                .HasMany(r => r.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);
            ///////
            modelBuilder.Entity<Toy>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Toys)
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<Order>()
               .HasMany(o => o.Payments)
               .WithOne(p => p.Order)
               .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(od => od.ShipDates)
                .WithOne(sd => sd.OrderDetail)
                .HasForeignKey(sd => sd.OrderDetailId);

            modelBuilder.Entity<ShipDate>()
                .HasKey(sd => sd.ShipDateId);
        }
    }
}
