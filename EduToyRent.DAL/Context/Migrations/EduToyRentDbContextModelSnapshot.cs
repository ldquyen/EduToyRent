﻿// <auto-generated />
using System;
using EduToyRent.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduToyRent.DAL.Context.Migrations
{
    [DbContext(typeof(EduToyRentDbContext))]
    partial class EduToyRentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EduToyRent.DAL.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("AccountEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.AccountVoucher", b =>
                {
                    b.Property<int>("AccountVoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountVoucherId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int>("VoucherId")
                        .HasColumnType("int");

                    b.HasKey("AccountVoucherId");

                    b.HasIndex("AccountId");

                    b.HasIndex("VoucherId");

                    b.ToTable("AccountVoucher");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ToyId")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ToyId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.DepositOrder", b =>
                {
                    b.Property<int>("DepositOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepositOrderId"));

                    b.Property<string>("BankCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RefundDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RefundMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StatusOrder")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DepositOrderId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("DepositOrder");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<decimal>("FinalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsRentalOrder")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ReceivePhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shipper")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipperPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusOrder")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<bool>("IsRental")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentalDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("RentalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToyId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ToyId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BankCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("AccountId");

                    b.HasIndex("OrderId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoke")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.RequestForm", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<string>("DenyReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ForRent")
                        .HasColumnType("bit");

                    b.Property<int?>("ProcessedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResponseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToyId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("ProcessedById");

                    b.HasIndex("ToyId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("ToyId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("AccountId");

                    b.HasIndex("ToyId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.ShipDate", b =>
                {
                    b.Property<int>("ShipDateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipDateId"));

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RefundDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShipStatus")
                        .HasColumnType("int");

                    b.HasKey("ShipDateId");

                    b.HasIndex("OrderDetailId")
                        .IsUnique();

                    b.ToTable("ShipDate");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Toy", b =>
                {
                    b.Property<int>("ToyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToyId"));

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRental")
                        .HasColumnType("bit");

                    b.Property<decimal?>("RentPricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RentPricePerTwoWeeks")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RentPricePerWeek")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("ToyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ToyId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Toy");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Used")
                        .HasColumnType("int");

                    b.Property<string>("VoucherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoucherId");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Account", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.AccountVoucher", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("AccountVouchers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Voucher", "Voucher")
                        .WithMany("AccountVouchers")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Cart", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithOne("Cart")
                        .HasForeignKey("EduToyRent.DAL.Entities.Cart", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.CartItem", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Toy", "Toy")
                        .WithMany("CartItems")
                        .HasForeignKey("ToyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.DepositOrder", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Order", "Order")
                        .WithOne("DepositOrders")
                        .HasForeignKey("EduToyRent.DAL.Entities.DepositOrder", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Order", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.OrderDetail", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Toy", "Toy")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ToyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Payment", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("Payments")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.RefreshToken", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.RequestForm", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("RequestForms")
                        .HasForeignKey("ProcessedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EduToyRent.DAL.Entities.Toy", "Toy")
                        .WithMany("RequestForms")
                        .HasForeignKey("ToyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Review", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Account", "Account")
                        .WithMany("Reviews")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Toy", "Toy")
                        .WithMany("Reviews")
                        .HasForeignKey("ToyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.ShipDate", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.OrderDetail", "OrderDetail")
                        .WithOne("ShipDates")
                        .HasForeignKey("EduToyRent.DAL.Entities.ShipDate", "OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Toy", b =>
                {
                    b.HasOne("EduToyRent.DAL.Entities.Category", "Category")
                        .WithMany("Toys")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduToyRent.DAL.Entities.Account", "Supplier")
                        .WithMany("Toys")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Account", b =>
                {
                    b.Navigation("AccountVouchers");

                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("RefreshTokens");

                    b.Navigation("RequestForms");

                    b.Navigation("Reviews");

                    b.Navigation("Toys");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Category", b =>
                {
                    b.Navigation("Toys");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Order", b =>
                {
                    b.Navigation("DepositOrders")
                        .IsRequired();

                    b.Navigation("OrderDetails");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.OrderDetail", b =>
                {
                    b.Navigation("ShipDates")
                        .IsRequired();
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Toy", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderDetails");

                    b.Navigation("RequestForms");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("EduToyRent.DAL.Entities.Voucher", b =>
                {
                    b.Navigation("AccountVouchers");
                });
#pragma warning restore 612, 618
        }
    }
}
