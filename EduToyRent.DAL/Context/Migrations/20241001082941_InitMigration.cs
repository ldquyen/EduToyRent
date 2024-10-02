using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduToyRent.DAL.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.VoucherId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "AccountVoucher",
                columns: table => new
                {
                    AccountVoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountVoucher", x => x.AccountVoucherId);
                    table.ForeignKey(
                        name: "FK_AccountVoucher_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountVoucher_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    StatusOrder = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    FinalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRentalOrder = table.Column<bool>(type: "bit", nullable: false),
                    ReceivePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shipper = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipperPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoke = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Toy",
                columns: table => new
                {
                    ToyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsRental = table.Column<bool>(type: "bit", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RentPricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RentPricePerWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RentPricePerTwoWeeks = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toy", x => x.ToyId);
                    table.ForeignKey(
                        name: "FK_Toy_Account_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toy_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepositOrder",
                columns: table => new
                {
                    DepositOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RefundMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefundDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositOrder", x => x.DepositOrderId);
                    table.ForeignKey(
                        name: "FK_DepositOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ToyId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItem_Toy_ToyId",
                        column: x => x.ToyId,
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ToyId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRental = table.Column<bool>(type: "bit", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Toy_ToyId",
                        column: x => x.ToyId,
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestForm",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToyId = table.Column<int>(type: "int", nullable: false),
                    ProcessedById = table.Column<int>(type: "int", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    ForRent = table.Column<bool>(type: "bit", nullable: false),
                    DenyReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForm", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RequestForm_Account_ProcessedById",
                        column: x => x.ProcessedById,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestForm_Toy_ToyId",
                        column: x => x.ToyId,
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToyId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Toy_ToyId",
                        column: x => x.ToyId,
                        principalTable: "Toy",
                        principalColumn: "ToyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipDate",
                columns: table => new
                {
                    ShipDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipDate", x => x.ShipDateId);
                    table.ForeignKey(
                        name: "FK_ShipDate_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Supplier" },
                    { 3, "Staff" },
                    { 4, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "AccountEmail", "AccountName", "AccountPassword", "Address", "IsBan", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { 1, "user@gmail.com", "user", "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", "HCM", false, "1234567890", 1 },
                    { 2, "supplier@gmail.com", "supplier", "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", "HCM", false, "1234567890", 2 },
                    { 3, "staff@gmail.com", "staff", "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", "HCM", false, "1234567890", 3 },
                    { 4, "admin@gmail.com", "admin", "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", "HCM", false, "1234567890", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountVoucher_AccountId",
                table: "AccountVoucher",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountVoucher_VoucherId",
                table: "AccountVoucher",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_AccountId",
                table: "Cart",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ToyId",
                table: "CartItem",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositOrder_OrderId",
                table: "DepositOrder",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ToyId",
                table: "OrderDetail",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AccountId",
                table: "Payment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForm_ProcessedById",
                table: "RequestForm",
                column: "ProcessedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForm_ToyId",
                table: "RequestForm",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AccountId",
                table: "Review",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ToyId",
                table: "Review",
                column: "ToyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipDate_OrderDetailId",
                table: "ShipDate",
                column: "OrderDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toy_CategoryId",
                table: "Toy",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Toy_SupplierId",
                table: "Toy",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountVoucher");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "DepositOrder");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RequestForm");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "ShipDate");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Toy");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
